using Colony_Management_System.Models.DbContext;
using Microsoft.Extensions.Configuration;
using PayPal.Api;
using PayPal.Exception;
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

            // Validate currency code
            if (string.IsNullOrWhiteSpace(currency) || currency.Length != 3)
            {
                throw new ArgumentException("Invalid currency code", nameof(currency));
            }

            // Validate URLs
            if (!Uri.IsWellFormedUriString(returnUrl, UriKind.Absolute))
            {
                throw new ArgumentException("Invalid return URL", nameof(returnUrl));
            }

            if (!Uri.IsWellFormedUriString(cancelUrl, UriKind.Absolute))
            {
                throw new ArgumentException("Invalid cancel URL", nameof(cancelUrl));
            }

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
                            total = amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture) // Ensure 2 decimal places with dot separator
                        },
                        description = "Opłata za kolonię",
                        item_list = new ItemList
                        {
                            items = new List<Item>
                            {
                                new Item
                                {
                                    name = "Kolonia",
                                    currency = currency,
                                    price = amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture), // Ensure 2 decimal places with dot separator
                                    quantity = "1",
                                    sku = "sku"
                                }
                            }
                        }
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            try
            {
                Console.WriteLine("Creating PayPal payment with the following details:");
                Console.WriteLine($"Amount: {amount.ToString("F2", System.Globalization.CultureInfo.InvariantCulture)}");
                Console.WriteLine($"Currency: {currency}");
                Console.WriteLine($"Return URL: {returnUrl}");
                Console.WriteLine($"Cancel URL: {cancelUrl}");

                return ExecuteWithRetry(() => payment.Create(apiContext));
            }
            catch (PaymentsException ex)
            {
                // Log the request and response for debugging
                Console.WriteLine("PayPal API Status Code: " + ex.StatusCode);
                Console.WriteLine("PayPal API Response: " + ex.Response);
                throw;
            }
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
                catch (PayPal.HttpException ex) when (ex.StatusCode == HttpStatusCode.ServiceUnavailable && retries < maxRetries )
                {
                    retries++;
                    Thread.Sleep(delayMilliseconds);
                }
            }
        }
    }
}