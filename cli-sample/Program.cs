// See https://aka.ms/new-console-template for more information

using System.Globalization;
using DotNetEnv;
using Paymongo.Sharp;
using Paymongo.Sharp.Checkouts.Entities;
using Paymongo.Sharp.Core.Entities;
using Paymongo.Sharp.Core.Enums;

namespace cli_sample;

public class Program
{
    public static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        var secretKey = Env.GetString("SECRET_KEY");

        // Initializing client
        var client = new PaymongoClient(secretKey);

        // Create a link object for request
        var checkout = new Checkout()
        {
            ReferenceNumber = "ABCD",
            Description = "Test payment checkout",
            Billing = new Billing
            {
                Name = "John Russell Camo",
                Email = "testmail@mail.com",
                Phone = "9282320392",
                Address = new Address
                {
                    Line1 = "Sa may bahay",
                    City = "Legazpi",
                    State = "Albay",
                    Country = "PH",
                    PostalCode = "4524"
                }
            },
            LineItems = new []
            {
                new LineItem
                {
                    Name = "TEst Item",
                    Amount = 50000,
                    Currency = Currency.Php,
                    Quantity = 1
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya
            }
        };

        // We create a link request
        var requestResult = await client.Checkouts.CreateCheckoutAsync(checkout);

        Console.WriteLine($"Pay here: {requestResult.CheckoutUrl}");
        Console.WriteLine("Waiting for payment..");

        // We wait for the payment to succeed
        while (true)
        {
            var getLink = await client.Checkouts.RetrieveCheckoutAsync(requestResult.Id);

            if (getLink.Payments.Any())
            {
                var payment = getLink.Payments.First();

                var platform = payment.Source["type"];
                var paymentDate = payment.PaidAt;
                var fee = (payment.Fee / 100).ToString("C", CultureInfo.InstalledUICulture);

                // We print successful payment
                Console.WriteLine($"Successfully paid on {paymentDate} using {platform} with fee: {fee}");
                break;
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}