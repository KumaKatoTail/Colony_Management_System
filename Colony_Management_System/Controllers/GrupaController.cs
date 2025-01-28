using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Services;

namespace Colony_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupaController : ControllerBase
    {
        private readonly IGrupaService _grupaService;

        public GrupaController(IGrupaService grupaService)
        {
            _grupaService = grupaService;
        }

        [HttpGet("kolonia/{koloniaId}")]
        public async Task<IActionResult> GetGrupyByKoloniaId(int koloniaId)
        {
            try
            {
                var grupy = await _grupaService.GetGrupyByKoloniaIdAsync(koloniaId);

                if (grupy == null || !grupy.Any())
                    return NotFound("No groups found for the given colony ID.");

                // Możesz dostosować zwracane dane, np. zwracać tylko wybrane pola
                var result = grupy.Select(g => new
                {
                    
                    g.Temat,
                    g.Opis,
                    
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("kolonia/{koloniaId}/wolne-miejsca")]
        public async Task<IActionResult> GetGrupyWithWolnemiejscaByKoloniaId(int koloniaId)
        {
            try
            {
                var grupy = await _grupaService.GetGrupyByKoloniaIdZSAsync(koloniaId);

                if (grupy == null || !grupy.Any())
                    return NotFound("No groups found for the given colony ID.");

                return Ok(grupy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
