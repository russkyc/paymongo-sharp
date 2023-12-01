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
        var client = new PaymongoClient(secretKey);

        // Create a link object for request
        var link = new Link
        {
            ReferenceNumber = "ABCD",
            Description = "Test payment link",
            Amount = 10000, // is an int variant, eg; if the amount is 100.00 then the value should be set as 10000
            Currency = Currency.Php
        };

        // We create a link request
        var requestResult = await client.Links.CreateLinkAsync(link);

        Console.WriteLine($"Pay here: {requestResult.CheckoutUrl}");
        Console.WriteLine("Waiting for payment..");

        // We wait for the payment to succeed
        while (true)
        {
            var getLink = await client.Links.GetLinkByReferenceNumberAsync(requestResult.ReferenceNumber);

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