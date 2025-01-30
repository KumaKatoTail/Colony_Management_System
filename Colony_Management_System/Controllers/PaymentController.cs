using Colony_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using Colony_Management_System.Models;
using System.Linq;
using System.Threading.Tasks;
using PayPal.Exception;

namespace Colony_Management_System.Controllers
{
    [Route("api/payments")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PayPalService _payPalService;
        private readonly PlatnoscService _platnoscService;

        public PaymentController(PayPalService payPalService, PlatnoscService platnoscService)
        {
            _payPalService = payPalService;
            _platnoscService = platnoscService;
        }

        // Akcja do tworzenia płatności (wystawienie API dla przycisku PayPal)
        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request)
        {
            string returnUrl = "https://localhost:44343/api/payments/execute";
            string cancelUrl = "https://localhost:44343/api/payments/cancel";

            // Sprawdzenie, czy `KoloniaDzieckoId` istnieje
            bool exists = await _platnoscService.KoloniaDzieckoExistsAsync(request.KoloniaDieckoId);
            if (!exists)
            {
                return BadRequest("Nie znaleziono dziecka o podanym ID.");
            }

            // Validate that the rodzajPlatnosci_id exists
            bool rodzajPlatnosciExists = await _platnoscService.RodzajPlatnosciExistsAsync(request.RodzajPlatnosciId);
            if (!rodzajPlatnosciExists)
            {
                return BadRequest("Nie znaleziono rodzaju płatności o podanym ID.");
            }

            // Tworzenie obiektu płatności w bazie
            var newPayment = new Platnosc
            {
                KoloniaDieckoId = request.KoloniaDieckoId, // Teraz na pewno istnieje
                RodzicId = request.RodzicId,
                Kwota = request.Amount,
                DataPlatnosci = DateTime.UtcNow,
                Opis = request.Description,
                NumerRef = "", // Tymczasowo pusty
                StatusId = 2, // Status "Oczekuje na płatność"
                Bramka = "PayPal",
                OplataPosr = 0.0,
                RodzajPlatnosciId = request.RodzajPlatnosciId, // Ensure this is set correctly
                WalutaId = request.WalutaId // Ensure this is set correctly
            };

            newPayment = await _platnoscService.AddPlatnoscAsync(newPayment);

            try
            {
                var payment = _payPalService.CreatePayment(request.Amount, "USD", returnUrl, cancelUrl);
                var approvalUrl = payment.links.FirstOrDefault(l => l.rel == "approval_url")?.href;
                if (approvalUrl == null)
                {
                    return BadRequest("Nie udało się uzyskać linku do płatności PayPal.");
                }

                // Aktualizacja numeru referencyjnego płatności w bazie
                newPayment.NumerRef = payment.id;
                await _platnoscService.UpdatePlatnoscStatusAsync(newPayment.Id, 1);

                return Ok(new { Url = approvalUrl });
            }
            catch (PaymentsException ex)
            {
                // Log the request and response for debugging
                Console.WriteLine("PayPal API Status Code: " + ex.StatusCode);
                Console.WriteLine("PayPal API Response: " + ex.Response);
                return BadRequest("Błąd podczas tworzenia płatności PayPal.");
            }
        }

        // Akcja po powrocie z PayPal
        [HttpGet("execute")]
        public async Task<IActionResult> ExecutePayment(string paymentId, string payerId)
        {
            var payment = _payPalService.ExecutePayment(paymentId, payerId);

            if (payment.state.ToLower() == "approved")
            {
                // Znalezienie płatności w bazie
                var platnosc = await _platnoscService.GetPlatnoscByPaymentIdAsync(paymentId);
                if (platnosc != null)
                {
                    platnosc.StatusId = 2; // Status "Zatwierdzona"
                    platnosc.IdTranzakcja = int.Parse(payment.transactions.First().related_resources.First().sale.id);
                    await _platnoscService.UpdatePlatnoscStatusAsync(platnosc.Id, 2);
                }

                return Ok("Payment successful.");
            }

            return BadRequest("Payment failed.");
        }

        [HttpGet("cancel")]
        public IActionResult CancelPayment()
        {
            return BadRequest("Payment was cancelled.");
        }

        // Get list of payments by RodzicId
        [HttpGet("by-rodzic/{rodzicId}")]
        public async Task<IActionResult> GetPlatnosciByRodzicId(int rodzicId)
        {
            var platnosci = await _platnoscService.GetPlatnosciByRodzicIdAsync(rodzicId);
            if (platnosci == null || platnosci.Count == 0)
                return NotFound("No payments found for the given parent.");

            return Ok(platnosci);
        }

        // Get single payment by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlatnoscById(int id)
        {
            var platnosc = await _platnoscService.GetPlatnoscByIdAsync(id);
            if (platnosc == null)
                return NotFound("Payment not found.");

            return Ok(platnosc);
        }
    }
}