using Microsoft.AspNetCore.Mvc;
using RecomendationSystemAPI.DTOs.Auth;
using RecomendationSystemAPI.Services.Interfaces;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var created = await _authService.RegisterAsync(dto);
            if (created == null) return BadRequest(new { error = "Email already in use" });
            return Ok(created);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var student = await _authService.LoginAsync(dto);
            if (student == null) return Unauthorized();
            return Ok(student);
        }
    }
}