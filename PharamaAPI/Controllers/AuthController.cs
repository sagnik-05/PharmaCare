using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaAPI.DTO;
using PharmaAPI.Services;
using System.Threading.Tasks;

namespace PharmaAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var result = await _authService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var token = await _authService.LoginAsync(model);
            if (token == null)
                return Unauthorized("Invalid credentials.");

            return Ok(new { Token = token, Message = "Login successful." });
        }

        [HttpGet("user")]
        public IActionResult GetUser()
        {
            return Ok(new { Message = "Authorized Access" });
        }
    }
}
