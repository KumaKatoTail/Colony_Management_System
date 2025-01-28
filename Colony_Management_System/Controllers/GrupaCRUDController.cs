using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Models;
using Colony_Management_System.Services;

namespace Colony_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GrupaCRUDController : ControllerBase
    {
        private readonly IGrupaServiceCRUD _grupaService;

        public GrupaCRUDController(IGrupaServiceCRUD grupaService)
        {
            _grupaService = grupaService;
        }

        // GET: api/Grupa
        [HttpGet]
        public async Task<IActionResult> GetAllGrupy()
        {
            try
            {
                var grupy = await _grupaService.GetAllGrupyAsync();
                return Ok(grupy);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Grupa/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGrupaById(int id)
        {
            try
            {
                var grupa = await _grupaService.GetGrupaByIdAsync(id);

                if (grupa == null)
                    return NotFound($"Grupa with ID {id} not found.");

                return Ok(grupa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Grupa
        [HttpPost]
        public async Task<IActionResult> CreateGrupa([FromBody] Grupa newGrupa)
        {
            try
            {
                var createdGrupa = await _grupaService.CreateGrupaAsync(newGrupa);
                return CreatedAtAction(nameof(GetGrupaById), new { id = createdGrupa.Id }, createdGrupa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Grupa/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGrupa(int id, [FromBody] Grupa updatedGrupa)
        {
            try
            {
                var grupa = await _grupaService.UpdateGrupaAsync(id, updatedGrupa);

                if (grupa == null)
                    return NotFound($"Grupa with ID {id} not found.");

                return Ok(grupa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Grupa/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrupa(int id)
        {
            try
            {
                var deleted = await _grupaService.DeleteGrupaAsync(id);

                if (!deleted)
                    return NotFound($"Grupa with ID {id} not found.");

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
