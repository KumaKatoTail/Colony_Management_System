using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Services;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;

        public AuthController(IUserAuthService userAuthService)
        {
            _userAuthService = userAuthService;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Email and password are required.");
            }

            try
            {
                // Wywołanie logowania
                var token = await _userAuthService.LoginAsync(model.Email, model.Password);

                // Zwrócenie tokenu JWT
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

    // Klasa modelu dla żądania logowania
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
