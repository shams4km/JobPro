// JobPro.Service/Auth/JwtSettings.cs
namespace JobPro.Service.Auth;

public class JwtSettings
{
    public string Secret { get; set; } = "your-secret-key";
    public int ExpiryMinutes { get; set; } = 60;
}