using Colony_Management_System.Models;
using Colony_Management_System.Models.DbContext;
using Colony_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Colony_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KoloniaController : ControllerBase
    {
        private readonly IKoloniaService _koloniaService;
        private readonly KoloniaDbContext _context; // Add this line to define the context

        public KoloniaController(IKoloniaService koloniaService, KoloniaDbContext context) // Modify the constructor to accept the context
        {
            _koloniaService = koloniaService;
            _context = context; // Initialize the context
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
                return CreatedAtAction(nameof(GetKoloniaNice), new { id = result.Id }, result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can add colonies.");
            }
        }



        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateKolonia(int id, [FromBody] Kolonia kolonia)
        {
            if (kolonia == null)
                return BadRequest("Kolonia data is required.");

            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                // Update the colony using the service
                var result = await _koloniaService.UpdateKoloniaAsync(id, kolonia, uprId.Value);

                // Return the updated colony
                return Ok(new
                {
                    result.Id,
                    result.FirmaId,
                    result.AdresId,
                    result.FormaId,
                    result.TerminOd,
                    result.TerminDo,
                    result.Nazwa,
                    result.TrasaWedrowna,
                    result.Opis,
                    result.Kraj
                });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can update colonies.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Kolonia not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

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
        //[HttpGet("{id}")]
        //[Authorize]
        //public async Task<IActionResult> GetKolonia(int id)
        //{
        //    var kolonia = await _koloniaService.GetKoloniaByIdAsync(id);
        //    if (kolonia == null)
        //        return NotFound("Kolonia not found.");
        //    return Ok(kolonia);
        //}

        [HttpGet("all")]
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
                    k.TerminDo,
                    k.Cena
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("future")]
        public async Task<IActionResult> GetFutureKolonieSummary()
        {
            try
            {
                // Pobierz listę wszystkich kolonii
                var kolonie = await _koloniaService.GetAllKolonieAsync();

                // Filtruj tylko przyszłe kolonie
                var futureKolonie = kolonie.Where(k => k.TerminOd > DateTime.Now).ToList();

                // Projekcja danych na uproszczony model
                var result = futureKolonie.Select(k => new
                {
                    k.Id,
                    k.Nazwa,
                    k.Opis,
                    k.TerminOd,
                    k.TerminDo,
                    k.Cena
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetKoloniaNice(int id)
        {
            var kolonia = await _context.Kolonia
                .Include(k => k.Firma)
                .Include(k => k.Adres)
                .Include(k => k.Forma)
                .Include(k => k.Adres.Ulica)
                .FirstOrDefaultAsync(k => k.Id == id);

            if (kolonia == null)
            {
                return NotFound();
            }

            // Dodaj sprawdzenie null dla każdego z obiektów
            var koloniaDto = new KoloniaDto
            {
                Id = kolonia.Id,
                Nazwa = kolonia.Nazwa,
                Opis = kolonia.Opis,
                Cena = (decimal)kolonia.Cena, // Explicit conversion from double to decimal
                TerminOd = kolonia.TerminOd,
                TerminDo = kolonia.TerminDo,
                TrasaWedrowna = kolonia.TrasaWedrowna,
                Kraj = kolonia.Kraj,
                Firma = kolonia.Firma != null ? new FirmaDto
                {
                    Id = kolonia.Firma.Id,
                    Nazwa = kolonia.Firma.Nazwa,
                    Adres = kolonia.Firma.Adres != null ? new AdresDto
                    {
                        Id = kolonia.Firma.Adres.Id,
                        UlicaNazwa = kolonia.Firma.Adres.Ulica?.ulica, // Sprawdzamy, czy Ulica jest null
                        NrDomu = kolonia.Firma.Adres.NrDomu,
                        NrMiesz = kolonia.Firma.Adres.NrMiesz
                    } : null
                } : null,
                Adres = kolonia.Adres != null ? new AdresDto
                {
                    Id = kolonia.Adres.Id,
                    UlicaNazwa = kolonia.Adres.Ulica?.ulica, // Sprawdzamy, czy Ulica jest null
                    NrDomu = kolonia.Adres.NrDomu,
                    NrMiesz = kolonia.Adres.NrMiesz
                } : null,
                Forma = kolonia.Forma != null ? new FormaDto
                {
                    Id = kolonia.Forma.Id,
                    RodzajWypoczynku = kolonia.Forma.RodzajWypoczynku
                } : null
            };

            return Ok(koloniaDto);
        }

        [HttpGet("future/{firmaId}")]
        public async Task<IActionResult> GetFutureKolonieByFirma(int firmaId)
        {
            try
            {
                // Pobierz listę wszystkich kolonii w danej firmie
                var kolonie = await _koloniaService.GetAllKolonieAsync();

                // Filtruj tylko przyszłe kolonie w danej firmie
                var futureKolonie = kolonie.Where(k => k.FirmaId == firmaId && k.TerminOd > DateTime.Now).ToList();

                // Projekcja danych na uproszczony model
                var result = futureKolonie.Select(k => new
                {
                    k.Id,
                    k.Nazwa,
                    k.Opis,
                    k.TerminOd,
                    k.TerminDo,
                    k.Cena
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("firma/{firmaId}")]
        public async Task<IActionResult> GetKolonieByFirma(int firmaId)
        {
            try
            {
                // Pobierz listę wszystkich kolonii w danej firmie
                var kolonie = await _koloniaService.GetAllKolonieAsync();

                // Filtruj kolonie dla danej firmy
                var filteredKolonie = kolonie.Where(k => k.FirmaId == firmaId).ToList();

                // Projekcja danych na uproszczony model
                var result = filteredKolonie.Select(k => new
                {
                    k.Id,
                    k.Nazwa,
                    k.Opis,
                    k.TerminOd,
                    k.TerminDo,
                    k.Cena
                });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("firma/{firmaId}")]
        [Authorize]
        public async Task<IActionResult> AddKoloniaToFirma(int firmaId, [FromBody] Kolonia kolonia)
        {
            if (kolonia == null)
                return BadRequest("Kolonia data is required.");

            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                // Przypisz FirmaId do kolonii
                kolonia.FirmaId = firmaId;

                var result = await _koloniaService.AddKoloniaAsync(kolonia, uprId.Value);
                return CreatedAtAction(nameof(GetKoloniaNice), new { id = result.Id }, result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can add colonies.");
            }
        }

        [HttpPut("{id}/firma/{firmaId}")]
        [Authorize]
        public async Task<IActionResult> UpdateKoloniaInFirma(int id, int firmaId, [FromBody] Kolonia kolonia)
        {
            if (kolonia == null)
                return BadRequest("Kolonia data is required.");

            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                // Sprawdź, czy kolonia należy do firmy
                var existingKolonia = await _koloniaService.GetKoloniaByIdAsync(id);
                if (existingKolonia == null || existingKolonia.FirmaId != firmaId)
                    return NotFound("Kolonia not found in the specified company.");

                var result = await _koloniaService.UpdateKoloniaAsync(id, kolonia, uprId.Value);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Only administrators can update colonies.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Kolonia not found.");
            }
        }

        [HttpDelete("{id}/firma/{firmaId}")]
        [Authorize]
        public async Task<IActionResult> DeleteKoloniaInFirma(int id, int firmaId)
        {
            var uprId = GetUprIdFromToken();
            if (uprId == null)
                return Unauthorized("Invalid token.");

            try
            {
                // Sprawdź, czy kolonia należy do firmy
                var existingKolonia = await _koloniaService.GetKoloniaByIdAsync(id);
                if (existingKolonia == null || existingKolonia.FirmaId != firmaId)
                    return NotFound("Kolonia not found in the specified company.");

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
