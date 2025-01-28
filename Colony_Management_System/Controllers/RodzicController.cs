using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Colony_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RodzicController : ControllerBase
    {
        private readonly RodzicService _rodzicService;
        private readonly KoloniaDbContext _context;

        public RodzicController(RodzicService rodzicService, KoloniaDbContext context)
        {
            _rodzicService = rodzicService;
            _context = context;
        }

        // GET: api/Rodzic
        [HttpGet]
        public async Task<IActionResult> GetRodzice()
        {
            var rodzice = await _rodzicService.GetRodziceListAsync();
            return Ok(rodzice);
        }

        // GET: api/Rodzic/ByKonto/{kontoId}
        [HttpGet("ByKonto/{kontoId}")]
        public async Task<IActionResult> GetRodzicByKontoId(int kontoId)
        {
            var rodzic = await _rodzicService.GetRodzicByIdAsync(kontoId);
            if (rodzic == null)
            {
                return NotFound();
            }
            return Ok(rodzic);
        }

        //// GET: api/Rodzic/ByAdres/{adresId}
        //[HttpGet("ByAdres/{adresId}")]
        //public async Task<IActionResult> GetRodzicByAdresId(int adresId)
        //{
        //    var rodzic = await _rodzicService.GetRodzicByAdresIdAsync(adresId);
        //    if (rodzic == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(rodzic);
        //}

        // POST: api/Rodzic
        [HttpPost]
        public async Task<IActionResult> PostRodzic(
    [FromQuery] string email,
    [FromQuery] string haslo,
    [FromQuery] string imie,
    [FromQuery] string nazwisko,
    [FromQuery] string telefon,
    [FromQuery] int adresId)
        {
            // Tworzymy nowe konto
            var konto = new Konto
            {
                Email = email,
                Haslo = haslo,
                UprId = 2  // UprId 2 oznacza rodzica
            };

            // Dodajemy konto do bazy danych
            _context.Konto.Add(konto);
            await _context.SaveChangesAsync();  // Zapisz konto w bazie, aby uzyskać kontoId

            // Tworzymy nowego rodzica
            var rodzic = new Rodzic
            {
                KontoId = konto.Id,  // KontoId z nowo utworzonego konta
                Imie = imie,
                Nazwisko = nazwisko,
                Telefon = telefon,
                Mail = email,
                AdresId = adresId
            };

            // Dodajemy rodzica do bazy danych
            _context.Rodzic.Add(rodzic);
            await _context.SaveChangesAsync();  // Zapisz rodzica w bazie

            // Zwracamy odpowiedź z nowym rodzicem
            return CreatedAtAction(nameof(GetRodzicByKontoId), new { kontoId = rodzic.KontoId }, rodzic);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRodzic(int id, [FromBody] UpdateRodzicDto dto)
        {
            var rodzic = await _rodzicService.GetRodzicByIdAsync(id);
            if (rodzic == null)
            {
                return NotFound();
            }

            // Sprawdzamy tylko te wartości, które zostały przekazane
            if (dto.KontoId.HasValue)
            {
                rodzic.KontoId = dto.KontoId.Value;
            }

            if (!string.IsNullOrEmpty(dto.Imie))
            {
                rodzic.Imie = dto.Imie;
            }

            if (!string.IsNullOrEmpty(dto.Nazwisko))
            {
                rodzic.Nazwisko = dto.Nazwisko;
            }

            if (!string.IsNullOrEmpty(dto.Telefon))
            {
                rodzic.Telefon = dto.Telefon;
            }

            if (!string.IsNullOrEmpty(dto.Mail))
            {
                rodzic.Mail = dto.Mail;
            }

            if (dto.AdresId.HasValue)
            {
                rodzic.AdresId = dto.AdresId.Value;
            }

            // Zapisujemy zmiany
            var updatedRodzic = await _rodzicService.UpdateRodzicAsync(id, rodzic);
            if (updatedRodzic == null)
            {
                return NotFound();
            }

            return Ok(updatedRodzic);
        }



        // DELETE: api/Rodzic/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRodzic(int id)
        {
            var success = await _rodzicService.DeleteRodzicAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
