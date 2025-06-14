using JobPro.Core.DTOs;

public interface IApplicationService
{
    Task Submit(ApplicationDTO dto);
    Task<IEnumerable<ApplicationDTO>> GetAll();
    Task<string> ExportToCsv();
}