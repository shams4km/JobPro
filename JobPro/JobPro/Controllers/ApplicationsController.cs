// JobPro.API/Controllers/ApplicationsController.cs
using Microsoft.AspNetCore.Mvc;
using JobPro.Core.DTOs;

namespace JobPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;
        private readonly IEmailService _emailService;

        public ApplicationsController(IApplicationService applicationService, IEmailService emailService)
        {
            _applicationService = applicationService;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> Apply([FromForm] ApplicationDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _applicationService.Submit(dto);
            await _emailService.SendUserEmail(dto);
            await _emailService.SendAdminEmail(dto);
            return Ok();
        }
    }
}