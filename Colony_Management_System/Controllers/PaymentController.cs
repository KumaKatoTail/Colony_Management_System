

    using Colony_Management_System.Services;
    using Microsoft.AspNetCore.Mvc;
    using Colony_Management_System.Services;

    namespace Colony_Management_System.Controllers
{
        public class PaymentController : Controller
        {
            private readonly PayPalService _payPalService;
            private readonly PlatnoscService _platnoscService;

            public PaymentController(PayPalService payPalService, PlatnoscService platnoscService)
            {
                _payPalService = payPalService;
                _platnoscService = platnoscService;
            }

            // Akcja do tworzenia płatności
            public IActionResult CreatePayment(decimal amount)
            {
                var payment = _payPalService.CreatePayment(amount, "USD");

                // Szukamy linku do zatwierdzenia płatności
                var approvalUrl = payment.links.FirstOrDefault(x => x.rel == "approval_url")?.href;
                if (approvalUrl != null)
                {
                    return Redirect(approvalUrl);
                }

                return View("Error");
            }

            // Akcja po powrocie z PayPal
            public IActionResult PaymentSuccess(string paymentId, string PayerID)
            {
                var payment = _payPalService.ExecutePayment(paymentId, PayerID);
                if (payment.state.ToLower() == "approved")
                {
                    // Możesz zapisać płatność w bazie danych, np. przypisać ją do kolonii
                    return View("Success");
                }

                return View("Failure");
            }

            // Akcja, gdy płatność została anulowana
            public IActionResult PaymentCancel()
            {
                return View("Cancel");
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


