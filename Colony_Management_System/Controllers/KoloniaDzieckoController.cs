using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Services;
using Colony_Management_System.Models;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KoloniaDzieckoController : ControllerBase
    {
        private readonly IKoloniaDzieckoService _koloniaDzieckoService;

        public KoloniaDzieckoController(IKoloniaDzieckoService koloniaDzieckoService)
        {
            _koloniaDzieckoService = koloniaDzieckoService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateKoloniaDziecko([FromBody] KoloniaDzieckoCreateDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            await _koloniaDzieckoService.CreateKoloniaDzieckoAsync(dto);
            return Ok("KoloniaDziecko created successfully.");
        }
        [HttpPost("add-by-id")]
        public async Task<IActionResult> CreateKoloniaDzieckoById(
    [FromQuery] int dzieckoId,
    [FromQuery] int grupaId,
    [FromQuery] int statusId,
    [FromQuery] DateTime dataZapisu)
        {
            try
            {
                // Tworzenie DTO
                var koloniaDzieckoDto = new KoloniaDzieckoCreateDTO
                {
                    DzieckoId = dzieckoId,
                    GrupaId = grupaId,
                    StatusId = statusId,
                    DataZapisu = dataZapisu
                };

                // Wywołanie serwisu z DTO
                await _koloniaDzieckoService.CreateKoloniaDzieckoAsync(koloniaDzieckoDto);

                return Ok("KoloniaDziecko created successfully using query parameters.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetKoloniaDzieckoById(int id)
        {
            var koloniaDziecko = await _koloniaDzieckoService.GetByIdAsync(id);
            if (koloniaDziecko == null)
                return NotFound("KoloniaDziecko not found.");

            return Ok(koloniaDziecko);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllKoloniaDziecko()
        {
            var koloniaDziecka = await _koloniaDzieckoService.GetAllAsync();
            return Ok(koloniaDziecka);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateKoloniaDziecko(int id, [FromBody] KoloniaDzieckoUpdateDTO dto)
        {
            if (dto == null || id != dto.Id)
                return BadRequest("Invalid data.");

            var koloniaDziecko = await _koloniaDzieckoService.GetByIdAsync(id);
            if (koloniaDziecko == null)
                return NotFound("KoloniaDziecko not found.");

            koloniaDziecko.DzieckoId = dto.DzieckoId;
            koloniaDziecko.GrupaId = dto.GrupaId;
            koloniaDziecko.StatusId = dto.StatusId;
            koloniaDziecko.DataZapisu = dto.DataZapisu;

            await _koloniaDzieckoService.UpdateKoloniaDzieckoAsync(koloniaDziecko);
            return Ok("KoloniaDziecko updated successfully.");
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKoloniaDziecko(int id)
        {
            await _koloniaDzieckoService.DeleteKoloniaDzieckoAsync(id);
            return Ok("KoloniaDziecko deleted successfully.");
        }
        [HttpGet("dzieci/{idRodzica}")]
        public async Task<IActionResult> GetKoloniedzieci(int idRodzica)
        {
            var dzieciNaKoloniach = await _koloniaDzieckoService.GetKoloniedzieciByRodzicIdAsync(idRodzica);
            if (dzieciNaKoloniach == null || !dzieciNaKoloniach.Any())
                return NotFound("No children found for the given parent.");

            return Ok(dzieciNaKoloniach);
        }

    }
}
