// JobPro.Service/ApplicationService.cs
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using JobPro.Core.DTOs;
using JobPro.Core.Models;
using JobPro.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace JobPro.Service
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ApplicationService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Submit(ApplicationDTO dto)
        {
            var application = _mapper.Map<Application>(dto);
            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ApplicationDTO>> GetAll()
        {
            var applications = await _context.Applications.ToListAsync();
            return _mapper.Map<IEnumerable<ApplicationDTO>>(applications);
        }

        public async Task<string> ExportToCsv()
        {
            var applications = await _context.Applications.ToListAsync();
            var csv = "Id,ФИО,Email,Телефон,Вакансия\n";
            foreach (var app in applications)
            {
                csv += $"{app.Id},{app.FullName},{app.Email},{app.Phone},{app.VacancyId}\n";
            }
            return csv;
        }
    }
}