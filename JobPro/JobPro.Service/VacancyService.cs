// JobPro.Service/VacancyService.cs

using AutoMapper;
using JobPro.Core.DTOs;
using JobPro.Data.Data;
using Microsoft.EntityFrameworkCore;

public class VacancyService : IVacancyService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public VacancyService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VacancyDTO>> Search(string keyword, string location)
    {
        var query = _context.Vacancies.AsQueryable();

        if (!string.IsNullOrEmpty(keyword))
            query = query.Where(v => v.Title.Contains(keyword) || v.Description.Contains(keyword));

        if (!string.IsNullOrEmpty(location))
            query = query.Where(v => v.Location.Contains(location));

        return await query.Select(v => _mapper.Map<VacancyDTO>(v)).ToListAsync();
    }

    public async Task<VacancyDTO> GetById(int id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);
        return _mapper.Map<VacancyDTO>(vacancy);
    }

    public async Task<IEnumerable<VacancyDTO>> GetAll()
    {
        var vacancies = await _context.Vacancies.ToListAsync();
        return _mapper.Map<IEnumerable<VacancyDTO>>(vacancies);
    }

    public async Task Delete(int id)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);
        if (vacancy != null)
        {
            _context.Vacancies.Remove(vacancy);
            await _context.SaveChangesAsync();
        }
    }

    public async Task Update(int id, VacancyDTO dto)
    {
        var vacancy = await _context.Vacancies.FindAsync(id);
        if (vacancy != null)
        {
            _mapper.Map(dto, vacancy);
            _context.Vacancies.Update(vacancy);
            await _context.SaveChangesAsync();
        }
    }
}
        
