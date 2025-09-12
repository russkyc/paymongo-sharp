// See https://aka.ms/new-console-template for more information

using System.Globalization;
using DotNetEnv;
using Paymongo.Sharp;
using Paymongo.Sharp.Core.Entities;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Features.Checkouts.Contracts;

namespace cli_sample;

#pragma warning disable CS8601
#pragma warning disable CS8602
#pragma warning disable CS8604

public class Program
{
    public static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        var secretKey = Env.GetString("SECRET_KEY");

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

        Console.WriteLine("Your cart:");
        foreach (var lineItem in products)
        {
            Console.WriteLine($"({lineItem.Quantity}){lineItem.Name} - {lineItem.Amount/100:C}");
        }

        Console.WriteLine("\n\nEnter your details for Checkout");
        
        Console.Write("Name: ");
        var name = Console.ReadLine();

        Console.Write("Email: ");
        var email = Console.ReadLine();

        Console.Write("Phone: ");
        var phone = Console.ReadLine();
        
        Console.Write("Street: ");
        var street = Console.ReadLine();
        
        Console.Write("City: ");
        var city = Console.ReadLine();
        
        Console.Write("State: ");
        var state = Console.ReadLine();
        
        Console.Write("Postal Code: ");
        var postalode = Console.ReadLine();
        
        Console.Write("Country: ");
        var country = Console.ReadLine();
        
        // Create a link object for request
        var checkout = new Checkout()
        {
            Data = new CheckoutData()
            {
                Attributes = new CheckoutAttributes()
                {
                    ReferenceNumber = "00928237",
                    Description = "Party Set Products",
                    Billing = new Billing
                    {
                        Name = name,
                        Email = email,
                        Phone = phone,
                        Address = new Address
                        {
                            Line1 = street,
                            City = city,
                            State = state,
                            PostalCode = postalode,
                            Country = country
                        }
                    },
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

        Console.WriteLine($"\n\nPay here: {requestResult.Data.Attributes.CheckoutUrl}");
        Console.WriteLine("Waiting for payment..");

        // We wait for the payment to succeed
        while (true)
        {
            var getLink = await client.Checkouts.RetrieveCheckoutAsync(requestResult.Data.Id);

            if (getLink.Data.Attributes.Payments.Any())
            {
                var payment = getLink.Data.Attributes.Payments.First();

                var platform = payment.Attributes.Source.Type;
                var paymentDate = payment.Attributes.PaidAt;
                var fee = (payment.Attributes.Fee / 100).ToString("C", CultureInfo.InstalledUICulture);

                // We print successful payment
                Console.WriteLine($"\n\nSuccessfully paid on {paymentDate} using {platform} with fee: {fee}");
                break;
            }

            await Task.Delay(TimeSpan.FromSeconds(1));
        }
    }
}