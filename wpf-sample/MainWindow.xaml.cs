﻿
using System.Linq;
using System.Windows;
using DotNetEnv;
using Paymongo.Sharp;
using Paymongo.Sharp.Core.Entities;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Features.Checkouts.Entities;
using Paymongo.Sharp.Features.Links.Entities;
using Paymongo.Sharp.Features.Sources.Entities;
using Paymongo.Sharp.Utilities;

#pragma warning disable CS8604

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

        private async void OnPayLink(object sender, RoutedEventArgs e)
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

            AmountTextBox.Text = string.Empty;
            StatusBlock.Text = string.Empty;
            
            var secretKey = Env.GetString("SECRET_KEY");
            var client = new PaymongoClient(secretKey);
            
            var link = new Link
            {
                Description = "Payment for",
                Amount = doubleAmount.ToLongAmount(),
                Currency = Currency.Php
            };

            var linkResult = await client.Links.CreateLinkAsync(link);
            var paymentWindow = new PaymentWindow(linkResult.CheckoutUrl);

            paymentWindow.Show();

            while (true)
            {
                var paymentStatus = await client.Links.RetrieveLinkAsync(linkResult.Id);
                
                if (!paymentStatus.Payments.Any())
                {
                    continue;
                }
                
                var payment = paymentStatus.Payments.FirstOrDefault();

                if (payment is null || payment.Status != PaymentStatus.Pending)
                {
                    continue;
                }

                StatusBlock.Text = $"Paid by {payment.Billing?.Name} on {payment.PaidAt} using {payment.Source?.Type}";
                
                paymentWindow.Close();
                
                break;
            }

        }
        
        private async void OnPayCheckout(object sender, RoutedEventArgs e)
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

            AmountTextBox.Text = string.Empty;
            StatusBlock.Text = string.Empty;
            
            var secretKey = Env.GetString("SECRET_KEY");
            var client = new PaymongoClient(secretKey);
            
            Checkout checkout = new Checkout()
            {
                Description = "Test Checkout",
                ReferenceNumber = "9282321A",
                LineItems =
                [
                    new LineItem
                    {
                        Name = "Give You Up",
                        Images = new []
                        {
                            "https://i.insider.com/602ee9ced3ad27001837f2ac?width=750&format=jpeg"
                        },
                        Quantity = 1,
                        Currency = Currency.Php,
                        Amount = doubleAmount.ToLongAmount()
                    }
                ],
                PaymentMethodTypes = new[]
                {
                    PaymentMethod.GCash,
                    PaymentMethod.Card,
                    PaymentMethod.Paymaya,
                    PaymentMethod.BillEase,
                    PaymentMethod.Dob,
                    PaymentMethod.GrabPay,
                    PaymentMethod.DobUbp
                }
            };
            
            Checkout checkoutResult = await client.Checkouts.CreateCheckoutAsync(checkout);
            var paymentWindow = new PaymentWindow(checkoutResult.CheckoutUrl);

            paymentWindow.Show();

            while (true)
            {
                var paymentStatus = await client.Checkouts.RetrieveCheckoutAsync(checkoutResult.Id);
                
                if (paymentStatus.Payments is null || !paymentStatus.Payments.Any())
                {
                    continue;
                }
                
                var payment = paymentStatus.Payments.FirstOrDefault();

                if (payment is null || payment.Status != PaymentStatus.Pending)
                {
                    continue;
                }

                StatusBlock.Text = $"Paid by {payment.Billing?.Name} on {payment.PaidAt} using {payment.Source?.Type}";
                
                paymentWindow.Close();
                
                break;
            }

        }
        
        private async void OnPayGcash(object sender, RoutedEventArgs e)
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

            AmountTextBox.Text = string.Empty;
            StatusBlock.Text = string.Empty;
            
            var secretKey = Env.GetString("SECRET_KEY");
            var client = new PaymongoClient(secretKey);
            
            // Arrange
            Source source = new Source
            {
                Amount = doubleAmount.ToLongAmount(),
                Description = "New Gcash Payment",
                Billing = new Billing
                {
                    Name = "TestName",
                    Email = "test@paymongo.com",
                    Phone = "9063364572",
                    Address = new Address
                    {
                        Line1 = "TestAddress1",
                        Line2 = "TestAddress2",
                        PostalCode = "4506",
                        State = "TestState",
                        City = "TestCity",
                        Country = "PH"
                    }
                },
                Redirect = new Redirect
                {
                    Success = "http://127.0.0.1",
                    Failed = "http://127.0.0.1"
                },
                Type = SourceType.GCash,
                Currency = Currency.Php
            };

            // Act
            var sourceResult = await client.Sources.CreateSourceAsync(source);
            var paymentWindow = new PaymentWindow(sourceResult.Redirect.CheckoutUrl);

            paymentWindow.Show();

            while (true)
            {
                var paymentStatus = await client.Sources.RetrieveSourceAsync(sourceResult.Id);
                
                if (paymentStatus.Status != SourceStatus.Chargeable)
                {
                    continue;
                }

                StatusBlock.Text = $"Chargeable on GCash by {paymentStatus.Billing.Name} on {paymentStatus.UpdatedAt}";
                
                paymentWindow.Close();
                
                break;
            }

        }
        
        private async void OnPayGrabPay(object sender, RoutedEventArgs e)
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

            AmountTextBox.Text = string.Empty;
            StatusBlock.Text = string.Empty;
            
            var secretKey = Env.GetString("SECRET_KEY");
            var client = new PaymongoClient(secretKey);
            
            // Arrange
            Source source = new Source
            {
                Amount = doubleAmount.ToLongAmount(),
                Description = "New GrabPay Payment",
                Billing = new Billing
                {
                    Name = "TestName",
                    Email = "test@paymongo.com",
                    Phone = "9063364572",
                    Address = new Address
                    {
                        Line1 = "TestAddress1",
                        Line2 = "TestAddress2",
                        PostalCode = "4506",
                        State = "TestState",
                        City = "TestCity",
                        Country = "PH"
                    }
                },
                Redirect = new Redirect
                {
                    Success = "http://127.0.0.1",
                    Failed = "http://127.0.0.1"
                },
                Type = SourceType.GrabPay,
                Currency = Currency.Php
            };

            // Act
            var sourceResult = await client.Sources.CreateSourceAsync(source);
            var paymentWindow = new PaymentWindow(sourceResult.Redirect.CheckoutUrl);

            paymentWindow.Show();

            while (true)
            {
                var paymentStatus = await client.Sources.RetrieveSourceAsync(sourceResult.Id);
                
                if (paymentStatus.Status != SourceStatus.Chargeable)
                {
                    continue;
                }

                StatusBlock.Text = $"Chargeable on GrabPay by {paymentStatus.Billing.Name} on {paymentStatus.UpdatedAt}";
                
                paymentWindow.Close();
                
                break;
            }

        }
        
    }
}