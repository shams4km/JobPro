// JobPro.API/Controllers/Admin/VacanciesController.cs
using Microsoft.AspNetCore.Mvc;
using JobPro.Core.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace JobPro.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class VacanciesController : ControllerBase
    {
        private readonly IVacancyService _vacancyService;

        public VacanciesController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vacancies = await _vacancyService.GetAll();
            return Ok(vacancies);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vacancyService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VacancyDTO dto)
        {
            await _vacancyService.Update(id, dto);
            return Ok();
        }
    }
}