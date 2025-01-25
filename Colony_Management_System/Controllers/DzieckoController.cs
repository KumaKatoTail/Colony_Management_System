using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Services;
using Microsoft.AspNetCore.Authorization;

namespace Colony_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DzieckoController : ControllerBase
    {
        private readonly IDzieckoService _dzieckoService;

        public DzieckoController(IDzieckoService dzieckoService)
        {
            _dzieckoService = dzieckoService;
        }

        [HttpGet("rodzic/{rodzicId}")]
        public async Task<IActionResult> GetDzieciByRodzicId(int rodzicId)
        {
            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            if (uprId != 2)
                return Forbid("Access is restricted to users with UprId = 2.");

            try
            {
                var dzieci = await _dzieckoService.GetDzieciByRodzicIdAsync(rodzicId);

                if (dzieci == null || !dzieci.Any())
                    return NotFound("No children found for the given parent ID.");

                // Zwracamy tylko imiona dzieci
                var result = dzieci.Select(d => new { d.Imie });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private int? GetUprIdFromToken()
        {
            // Zakładamy, że token zawiera informację o UprId w claimach
            var claim = User.Claims.FirstOrDefault(c => c.Type == "UprId");
            return claim != null ? int.Parse(claim.Value) : (int?)null;
        }
    }
}
