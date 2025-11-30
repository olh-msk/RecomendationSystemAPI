using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RecomendationSystemAPI.DTOs.Auth;
using RecomendationSystemAPI.Services.Interfaces;
using RecomendationSystemAPI.Helpers;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RecomendationSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
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
            var student = await _authService.AuthenticateAsync(dto);
            if (student == null) return Unauthorized(new { message = "Invalid credentials" });

            // create JWT token
            var jwtKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key missing");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var signing = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),
                new Claim(ClaimTypes.Name, student.FullName),
                new Claim(ClaimTypes.Email, student.Email),
                new Claim(ClaimTypes.Role, student.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims, // pass IEnumerable<Claim> here (fixed)
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: signing);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { token = tokenString, student = DtoMapper.ToDto(student) });
        }
    }
}