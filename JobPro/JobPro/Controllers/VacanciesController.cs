// JobPro.API/Controllers/VacanciesController.cs
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using JobPro.Core.DTOs;

namespace JobPro.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        public VacanciesController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string keyword, string location)
        {
            var vacancies = await _vacancyService.Search(keyword, location);
            return Ok(vacancies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var vacancy = await _vacancyService.GetById(id);
            return Ok(vacancy);
        }
    }
}