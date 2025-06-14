using JobPro.Core.DTOs;

public interface IEmailService
{
    Task SendUserEmail(ApplicationDTO dto);
    Task SendAdminEmail(ApplicationDTO dto);
}