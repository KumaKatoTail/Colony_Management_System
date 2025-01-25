using Colony_Management_System.Models;
using Colony_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoloniaController : ControllerBase
    {
        private readonly IKoloniaService _koloniaService;

        public KoloniaController(IKoloniaService koloniaService)
        {
            _koloniaService = koloniaService;
        }

        // Dodawanie nowej kolonii
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddKolonia([FromBody] Kolonia kolonia)
        {
            if (kolonia == null)
                return BadRequest("Kolonia data is required.");

            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                // Ustawiamy wartości domyślne dla pól, które mają być przypisane odgórnie
                kolonia.FirmaId = 1;  // Przykładowe przypisanie FirmaId
                kolonia.AdresId = 2;   // Przykładowe przypisanie AdresId
                kolonia.FormaId = 3;   // Przykładowe przypisanie FormaId
                kolonia.TerminOd = DateTime.Now;  // Przykładowa data rozpoczęcia
                kolonia.TerminDo = DateTime.Now.AddMonths(1);  // Przykładowa data zakończenia
                kolonia.Cena = 0;  // Przykładowa cena

                // Pozwalamy użytkownikowi uzupełnić tylko dane specyficzne dla kolonii
                var result = await _koloniaService.AddKoloniaAsync(kolonia, uprId.Value);
                return CreatedAtAction(nameof(GetKolonia), new { id = result.Id }, result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can add colonies.");
            }
        }



        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateKolonia(int id, [FromBody] Kolonia kolonia)
        //{
        //    if (kolonia == null)
        //        return BadRequest("Kolonia data is required.");

        //    var uprId = GetUprIdFromToken();
        //    if (uprId == null)
        //        return Unauthorized("Invalid token.");

        //    try
        //    {
        //        // Sprawdzamy, czy kolonia istnieje w bazie danych
        //        var existingKolonia = await _koloniaService.GetKoloniaByIdAsync(id);
        //        if (existingKolonia == null)
        //            return NotFound("Kolonia not found.");

        //        // Jeśli kolonia istnieje, aktualizujemy jej dane
        //        existingKolonia.Nazwa = kolonia.Nazwa ?? existingKolonia.Nazwa;
        //        existingKolonia.TrasaWedrowna = kolonia.TrasaWedrowna ?? existingKolonia.TrasaWedrowna;
        //        existingKolonia.Opis = kolonia.Opis ?? existingKolonia.Opis;
        //        existingKolonia.Kraj = kolonia.Kraj ?? existingKolonia.Kraj;
        //        existingKolonia.TerminOd = kolonia.TerminOd != default ? kolonia.TerminOd : existingKolonia.TerminOd;
        //        existingKolonia.TerminDo = kolonia.TerminDo != default ? kolonia.TerminDo : existingKolonia.TerminDo;

        //        // Zaktualizowanie kolonii
        //        var result = await _koloniaService.UpdateKoloniaAsync()    .UpdateKoloniaAsync(existingKolonia, uprId.Value);

        //        // Zwracamy tylko dane z tabeli Kolonia (bez powiązanych tabel)
        //        return Ok(new
        //        {
        //            result.Id,
        //            result.FirmaId,
        //            result.AdresId,
        //            result.FormaId,
        //            result.TerminOd,
        //            result.TerminDo,
        //            result.Nazwa,
        //            result.TrasaWedrowna,
        //            result.Opis,
        //            result.Kraj
        //        });
        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        return Unauthorized("Only administrators can update colonies.");
        //    }
        //}


        // Usuwanie kolonii
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteKolonia(int id)
        {
            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token." + uprId);

            try
            {
                var success = await _koloniaService.DeleteKoloniaAsync(id, uprId.Value);
                if (!success)
                    return NotFound("Kolonia not found.");
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can delete colonies.");
            }
        }

        // Pobieranie szczegółów kolonii
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetKolonia(int id)
        {
            var kolonia = await _koloniaService.GetKoloniaByIdAsync(id);
            if (kolonia == null)
                return NotFound("Kolonia not found.");
            return Ok(kolonia);
        }

        [HttpGet]
        public async Task<IActionResult> GetKolonieSummary()
        {
            try
            {
                // Pobierz listę wszystkich kolonii
                var kolonie = await _koloniaService.GetAllKolonieAsync();

                // Projekcja danych na uproszczony model
                var result = kolonie.Select(k => new
                {
                    k.Id,
                    k.Nazwa,
                    k.Opis,
                    k.TerminOd,
                    k.Cena
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Helper method to get UprId from token
        private int? GetUprIdFromToken()
        {
            // Odczytanie claimu "UprId" z tokenu
            var uprIdClaim = User.FindFirst("UprId")?.Value;

            // Próba konwersji na liczbę całkowitą
            if (int.TryParse(uprIdClaim, out var uprId))
                return uprId;

            return null; // Zwraca null, jeśli UprId nie istnieje lub nie jest liczbą
        }

    }
}
