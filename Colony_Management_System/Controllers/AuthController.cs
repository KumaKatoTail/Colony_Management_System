using Colony_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Endpoint do logowania użytkownika
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginViewModel model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Haslo))
            {
                return BadRequest("Email and password are required.");
            }

            var result = await _userService.Authenticate(model.Email, model.Haslo);

            if (!result.Success)
            {
                return Unauthorized(result.ErrorMessage);
            }

            return Ok(new { Token = result.Token });
        }

        // Endpoint do weryfikacji tokenu
        [HttpPost("CheckToken")]
        [Authorize]
        public IActionResult CheckToken()
        {
            return Ok(new { Message = "Token is valid." });
        }
    }

    // Model do logowania
    public class LoginViewModel
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
    }
}
