using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Colony_Management_System.Models;

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

        // Pobieranie dzieci przypisanych do rodzica
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

                return Ok(dzieci);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Pobieranie dziecka po ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDzieckoById(int id)
        {
            var dziecko = await _dzieckoService.GetDzieckoByIdAsync(id);
            if (dziecko == null)
                return NotFound("Child not found.");
            return Ok(dziecko);
        }

        // Dodawanie nowego dziecka
        [HttpPost]
        public async Task<IActionResult> AddDziecko([FromBody] Dziecko dziecko)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var newDziecko = await _dzieckoService.AddDzieckoAsync(dziecko);
                return CreatedAtAction(nameof(GetDzieckoById), new { id = newDziecko.Id }, newDziecko);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Aktualizacja danych dziecka
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDziecko(int id, [FromBody] Dziecko dziecko)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                dziecko.Id = id; // Upewniamy się, że ID jest poprawnie ustawione
                var updatedDziecko = await _dzieckoService.UpdateDzieckoAsync(dziecko);
                if (updatedDziecko == null)
                    return NotFound("Child not found.");
                return Ok(updatedDziecko);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Usuwanie dziecka
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDziecko(int id)
        {
            try
            {
                var result = await _dzieckoService.DeleteDzieckoAsync(id);
                if (!result)
                    return NotFound("Child not found.");
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Pobieranie UprId z tokena
        private int? GetUprIdFromToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "UprId");
            return claim != null ? int.Parse(claim.Value) : (int?)null;
        }
    }
}
