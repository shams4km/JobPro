// JobPro.API/Controllers/Admin/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using JobPro.Core.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace JobPro.Controllers.Admin
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/admin/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpPut("{id}/block")]
        public async Task<IActionResult> Block(int id)
        {
            await _userService.Block(id);
            return Ok();
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> ChangeRole(int id, [FromBody] string role)
        {
            await _userService.ChangeRole(id, role);
            return Ok();
        }
    }
}