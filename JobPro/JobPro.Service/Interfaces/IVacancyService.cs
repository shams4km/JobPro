using JobPro.Core.DTOs;

public interface IVacancyService
{
    Task<IEnumerable<VacancyDTO>> Search(string keyword, string location);
    Task<VacancyDTO> GetById(int id);
    Task<IEnumerable<VacancyDTO>> GetAll();
    Task Delete(int id);
    Task Update(int id, VacancyDTO dto);
}