// See https://aka.ms/new-console-template for more information

using System.Globalization;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Features.Checkouts.Contracts;
using Paymongo.Sharp.Utilities;

namespace Paymongo.Sharp.Console.Sample;

#pragma warning disable CS8601
#pragma warning disable CS8602
#pragma warning disable CS8604

public static class Program
{
    public static async Task Main(string[] args)
    {
        System.Console.Write("ApiKey (Secret Key): ");
        var secretKey = System.Console.ReadLine().Trim();

        // Initializing client
        var client = new PaymongoClient(secretKey);

        var products = new[]
        {
            new LineItem
            {
                Name = "Boxes",
                Amount = 5000,
                Currency = Currency.Php,
                Quantity = 25
            },
            new LineItem
            {
                Name = "Cups",
                Amount = 2000,
                Currency = Currency.Php,
                Quantity = 30
            },
            new LineItem
            {
                Name = "Party Hats",
                Amount = 200,
                Currency = Currency.Php,
                Quantity = 20
            }
        };

        System.Console.WriteLine("Your cart:");
        foreach (var lineItem in products)
        {
            System.Console.WriteLine($"({lineItem.Quantity}){lineItem.Name} - {lineItem.Amount/100:C}");
        }

        // Create a link object for request
        var checkout = new Checkout()
        {
            Data = new CheckoutData()
            {
                Attributes = new CheckoutAttributes()
                {
                    ReferenceNumber = "00928237",
                    Description = "Party Set Products",
                    LineItems = products,
                    PaymentMethodTypes = new[]
                    {
                        PaymentMethodType.GCash,
                        PaymentMethodType.Card,
                        PaymentMethodType.Paymaya
                    }
                }
            }
        };

        // We create a link request
        var requestResult = await client.Checkouts.CreateCheckoutAsync(checkout);

        System.Console.WriteLine($"\n\nPay here: {requestResult.Data.Attributes.CheckoutUrl}");
        System.Console.WriteLine("Waiting for transaction to complete..");

        // We wait for the payment to succeed
        while (true)
        {
            var getLink = await client.Checkouts.RetrieveCheckoutAsync(requestResult.Data.Id);

            if (getLink.Data.Attributes.Payments.Any())
            {
                var payment = getLink.Data.Attributes.Payments.First();

                var platform = payment.Attributes.Source.Type;
                var paymentDate = payment.Attributes.PaidAt;
                var fee = payment.Attributes.Fee.ToDecimalAmount().ToString("C", CultureInfo.InstalledUICulture);

                // We print successful payment
                System.Console.WriteLine($"\n\nSuccessfully paid on {paymentDate} using {platform} with fee: {fee}");
                break;
            }

            await Task.Delay(TimeSpan.FromMilliseconds(500));
        }
    }
}