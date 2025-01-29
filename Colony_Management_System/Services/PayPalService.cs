using Colony_Management_System.Models.DbContext;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using PayPalHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace Colony_Management_System.Services
{
    public class PayPalService
    {
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly KoloniaDbContext _context;

        public PayPalService(IConfiguration configuration, KoloniaDbContext context)
        {
            _clientId = configuration["PayPal:ClientId"];
            _clientSecret = configuration["PayPal:ClientSecret"];
            _context = context;
        }

        private APIContext GetAPIContext()
        {
            var config = new Dictionary<string, string>
            {
                { "mode", "sandbox" } // Set to "live" in production
            };
            var accessToken = new OAuthTokenCredential(_clientId, _clientSecret, config).GetAccessToken();
            return new APIContext(accessToken);
        }

        public Payment CreatePayment(decimal amount, string currency, string returnUrl, string cancelUrl)
        {
            var apiContext = GetAPIContext();

            var payment = new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        amount = new Amount
                        {
                            currency = currency,
                            total = amount.ToString("F2") // Ensure 2 decimal places
                        },
                        description = "Opłata za kolonię"
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            return ExecuteWithRetry(() => payment.Create(apiContext));
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            var apiContext = GetAPIContext();
            var paymentExecution = new PaymentExecution { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            return ExecuteWithRetry(() => payment.Execute(apiContext, paymentExecution));
        }

        private T ExecuteWithRetry<T>(Func<T> action, int maxRetries = 3, int delayMilliseconds = 1000)
        {
            int retries = 0;
            while (true)
            {
                try
                {
                    return action();
                }
                catch (HttpException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable && retries < maxRetries )
                {
                    retries++;
                    Thread.Sleep(delayMilliseconds);
                }
            }
        }
    }
}