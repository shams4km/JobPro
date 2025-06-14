// JobPro.API/Controllers/Admin/ApplicationsController.cs

using System.Text;
using Microsoft.AspNetCore.Mvc;
using JobPro.Core.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace JobPro.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _applicationService.GetAll();
            return Ok(applications);
        }

        [HttpGet("export")]
        public async Task<IActionResult> Export()
        {
            var csv = await _applicationService.ExportToCsv();
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "applications.csv");
        }
    }
}