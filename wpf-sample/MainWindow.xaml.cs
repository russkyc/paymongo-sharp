
using System;
using System.Linq;
using System.Windows;
using DotNetEnv;
using Paymongo.Sharp;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Links.Entities;

namespace WpfSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Env.TraversePath().Load();
        }

        private async void OnPay(object sender, RoutedEventArgs e)
        {
            var isDouble = decimal.TryParse(AmountTextBox.Text, out decimal doubleAmount);

            if (!isDouble)
            {
                return;
            }

            if (doubleAmount < 100)
            {
                return;
            }

            var amount = ConvertToInt(doubleAmount);
            
            AmountTextBox.Text = string.Empty;
            StatusBlock.Text = string.Empty;
            
            var secretKey = Env.GetString("SECRET_KEY");
            var client = new PaymongoClient(secretKey);
            
            var link = new Link
            {
                Description = "Payment for",
                Amount = amount,
                Currency = Currency.Php
            };

            var linkResult = await client.Links.CreateLinkAsync(link);
            var paymentWindow = new PaymentWindow(linkResult.CheckoutUrl);

            paymentWindow.Show();

            while (true)
            {
                var paymentStatus = await client.Links.RetrieveLinkAsync(linkResult.Id);
                
                if (!paymentStatus.Payments!.Any())
                {
                    continue;
                }
                
                var payment = paymentStatus.Payments!.First();

                if (payment.Status != PaymentStatus.Paid)
                {
                    continue;
                }

                StatusBlock.Text = $"Paid by {payment.Billing!.Name} on {payment.PaidAt} using {payment.Source!["type"]}";
                
                paymentWindow.Close();
                
                break;
            }

        }
        
        int ConvertToInt(decimal decimalValue)
        {
            int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(decimalValue)[3])[2];
            decimal factor = (decimal)Math.Pow(10, decimalPlaces == 0 ? 2 : decimalPlaces);
            int intValue = (int)(decimalValue * factor);
            return intValue;
        }
    }
}