// JobPro.API/Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using JobPro.Core.DTOs;

namespace JobPro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtGenerator _jwtGenerator;

        public AuthController(IUserService userService, IJwtGenerator jwtGenerator)
        {
            _userService = userService;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.Register(dto);
            var token = _jwtGenerator.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.Login(dto);
            var token = _jwtGenerator.GenerateToken(user);
            return Ok(new { Token = token });
        }
    }
    
}