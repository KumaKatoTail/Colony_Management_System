
using PayPal.Api;
using Microsoft.Extensions.Options;
using Colony_Management_System.Services;

namespace Colony_Management_System.Services
{
    public class PayPalService
    {
        private readonly PayPalSettings _payPalSettings;

        public PayPalService(IOptions<PayPalSettings> payPalSettings)
        {
            _payPalSettings = payPalSettings.Value;
        }

        private APIContext GetApiContext()
        {
            var accessToken = new OAuthTokenCredential(_payPalSettings.ClientId, _payPalSettings.ClientSecret).GetAccessToken();
            return new APIContext(accessToken);
        }

        public Payment CreatePayment(decimal amount, string currency)
        {
            var apiContext = GetApiContext();

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer
                {
                    payment_method = "paypal"
                },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = currency,
                            total = amount.ToString()
                        },
                        description = "Płatność za kolonie"
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = "https://yourapp.com/payment/success",
                    cancel_url = "https://yourapp.com/payment/cancel"
                }
            };

            return payment.Create(apiContext);
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = GetApiContext();
            var paymentExecution = new PaymentExecution { payer_id = payerId };
            var payment = new Payment { id = paymentId };

            return payment.Execute(apiContext, paymentExecution);
        }
    }
}

