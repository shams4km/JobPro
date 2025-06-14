using System.Net;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using JobPro.Data.Data;
using JobPro.Service.Auth;
using JobPro.Core.DTOs;
using AutoMapper;
using System.Text;
using JobPro;
using JobPro.Service;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// 1. Настройка сервисов
var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

// 2. Регистрация контекста БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// 3. Регистрация сервисов
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// 4. Регистрация бизнес-логики
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddSingleton<IJwtGenerator, JwtGenerator>();
builder.Services.AddSingleton<IEmailSender, EmailService>();

// 5. JWT аутентификация
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
    };
});
builder.Services.AddAuthorization();

// 6. Статические файлы и CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader();
    });
});

var app = builder.Build();

// 7. Применение миграций при старте
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// 8. Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

// 9. Маршруты
app.MapControllers();

// 10. Глобальная обработка ошибок
app.UseExceptionHandler(options =>
{
    options.Run(async context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        var ex = context.Features.Get<IExceptionHandlerFeature>();
        if (ex != null)
        {
            var error = new { Message = ex.Error.Message };
            await context.Response.WriteAsJsonAsync(error);
        }
    });
});

app.Run();