// See https://aka.ms/new-console-template for more information

using System.Globalization;
using DotNetEnv;
using Paymongo.Sharp;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Links.Entities;

namespace cli_sample;

public class Program
{
    public static async Task Main(string[] args)
    {
        Env.TraversePath().Load();
        var secretKey = Env.GetString("SECRET_KEY");

        // Initializing client
        var client = new PaymongoClient(secretKey); // replace with your secret key

        // Create a link object for request
        var link = new Link
        {
            Description = "Test payment link", // Just the description
            Amount = 10000, // is an int variant, eg; if the amount is 100.00 then the value should be set as 10000
            Currency = Currency.Php // The only currency Paymongo supports
        };

        // We create a link request
        var requestResult = await client.Links.CreateLinkAsync(link);

        // The link object contains the checkout link that we can use
        Console.WriteLine($"Pay here: {requestResult.CheckoutUrl}");
        Console.WriteLine("Waiting for payment..");

        // We wait for the payment to succeed
        while (true)
        {
            // We retrieve the payment from the server using the id
            var getLink = await client.Links.RetrieveLinkAsync(requestResult.Id);

            // We check to see if there are any payments made
            if (getLink.Payments.Any())
            {
                // We get the payment info if payment has been made
                var payment = getLink.Payments.First();

                var platform = payment.Source["type"];
                var paymentDate = payment.PaidAt;
                var fee = (payment.Fee / 100).ToString("C", CultureInfo.InstalledUICulture);

                // We print successful payment
                Console.WriteLine($"Successfully paid on {paymentDate} using {platform} with fee: {fee}");
                break;
            }
        }
    }
}