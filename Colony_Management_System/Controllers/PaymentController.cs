namespace Colony_Management_System.Controllers
{
    using Colony_Management_System.Services;
    using Microsoft.AspNetCore.Mvc;
    using Colony_Management_System.Services;

    namespace YourAppNamespace.Controllers
    {
        public class PaymentController : Controller
        {
            private readonly PayPalService _payPalService;

            public PaymentController(PayPalService payPalService)
            {
                _payPalService = payPalService;
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
        }
    }

}
