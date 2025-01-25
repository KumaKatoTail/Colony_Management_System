using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Services;

namespace Colony_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KontoController : ControllerBase
    {
        private readonly IKontoService _kontoService;
        private readonly IKontoService2 _kontoService2;

        public KontoController(IKontoService kontoService)
        {
            _kontoService = kontoService;
        }

        [HttpPost("zalogowane")]
        public async Task<IActionResult> GetZalogowaneKonto([FromBody] LoginRequest request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Haslo))
                return BadRequest("Email and password are required.");

            var kontoDetails = await _kontoService.GetZalogowaneKontoDetailsAsync(request.Email, request.Haslo);

            if (kontoDetails == null)
                return NotFound("Invalid email or password.");

            return Ok(kontoDetails);
        }
    

        [HttpGet("zalogowane2")]
        public async Task<IActionResult> GetZalogowaneKonto()
        {
            // Odczyt emaila z tokena JWT
            var email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

            if (string.IsNullOrEmpty(email))
                return Unauthorized("Email not found in token.");

            // Pobranie szczegółów konta na podstawie emaila
            var kontoDetails = await _kontoService2.GetZalogowaneKontoDetailsByEmailAsync(email);

            if (kontoDetails == null)
                return NotFound("Account not found.");

            return Ok(kontoDetails);
        }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Haslo { get; set; }
    }
}
}
