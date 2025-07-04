// MIT License
// 
// Copyright (c) 2023 Russell Camo (@russkyc)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using Paymongo.Sharp.Checkouts.Entities;

namespace Paymongo.Sharp.Tests.Integration;

#pragma warning disable CS8604

public class CheckoutApiTests
{
    private readonly IPaymongoClient _client;

    public CheckoutApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }
    
    [Fact]
    async Task CreateCheckoutSession()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            CancelUrl = "http://127.0.0.1",
            SuccessUrl = "http://127.0.0.1",
            ReferenceNumber = "9282321A",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "Give You Up",
                    Images = new []
                    {
                        "https://i.insider.com/602ee9ced3ad27001837f2ac?width=750&format=jpeg"
                    },
                    Quantity = 1000,
                    Currency = Currency.Php,
                    Amount = 100
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya,
                PaymentMethod.BillEase,
                PaymentMethod.Dob,
                PaymentMethod.GrabPay,
                PaymentMethod.DobUbp
            },
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
            Metadata = new Dictionary<string, string>()
            {
                {"testKey","Test Value"}
            },
            SendEmailReceipt = true,
            ShowDescription = true,
            ShowLineItems = false
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        
        // Assert
        checkoutResult.Should().NotBeNull();
        checkoutResult.Id.Should().NotBeNullOrEmpty();
        checkoutResult.CheckoutUrl.Should().NotBeNullOrEmpty();
        checkoutResult.Status.Should().Be(CheckoutStatus.Active);
        checkoutResult.Billing.Should().BeEquivalentTo(checkout.Billing);
        checkoutResult.Metadata.Should().BeEquivalentTo(checkout.Metadata);
        checkoutResult.LineItems.Should().BeEquivalentTo(checkout.LineItems);
        checkoutResult.Description.Should().BeEquivalentTo(checkout.Description);
        checkoutResult.PaymentMethodTypes.Should().BeEquivalentTo(checkout.PaymentMethodTypes);

    }
    
    [Fact]
    async Task CreateAndRetrieveCheckoutSession()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            CancelUrl = "http://127.0.0.1",
            SuccessUrl = "http://127.0.0.1",
            ReferenceNumber = "9282321A",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "Give You Up",
                    Images = new []
                    {
                        "https://i.insider.com/602ee9ced3ad27001837f2ac?width=750&format=jpeg"
                    },
                    Quantity = 1000,
                    Currency = Currency.Php,
                    Amount = 100
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya,
                PaymentMethod.BillEase,
                PaymentMethod.Dob,
                PaymentMethod.GrabPay,
                PaymentMethod.DobUbp
            },
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
            Metadata = new Dictionary<string, string>()
            {
                {"testKey","Test Value"}
            },
            SendEmailReceipt = true,
            ShowDescription = true,
            ShowLineItems = false
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        Checkout getCheckoutResult = await _client.Checkouts.RetrieveCheckoutAsync(checkoutResult.Id);
        
        // Assert
        getCheckoutResult.Should().NotBeNull();
        getCheckoutResult.Should().BeEquivalentTo(checkoutResult);

    }
    
    [Fact]
    async Task CreateAndExpireCheckoutSession()
    {
        // Arrange
        Checkout checkout = new Checkout()
        {
            Description = "Test Checkout",
            CancelUrl = "http://127.0.0.1",
            SuccessUrl = "http://127.0.0.1",
            ReferenceNumber = "9282321A",
            LineItems = new []
            {
                new LineItem
                {
                    Name = "Give You Up",
                    Images = new []
                    {
                        "https://i.insider.com/602ee9ced3ad27001837f2ac?width=750&format=jpeg"
                    },
                    Quantity = 1000,
                    Currency = Currency.Php,
                    Amount = 10000
                }
            },
            PaymentMethodTypes = new[]
            {
                PaymentMethod.GCash,
                PaymentMethod.Card,
                PaymentMethod.Paymaya,
                PaymentMethod.BillEase,
                PaymentMethod.Dob,
                PaymentMethod.GrabPay,
                PaymentMethod.DobUbp
            },
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
            Metadata = new Dictionary<string, string>()
            {
                {"testKey","Test Value"}
            },
            SendEmailReceipt = true,
            ShowDescription = true,
            ShowLineItems = false
        };
        
        // Act
        Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
        Checkout getExpiredCheckoutResult = await _client.Checkouts.ExpireCheckoutAsync(checkoutResult.Id);
        
        // Assert
        checkoutResult.Should().NotBeNull();
        checkoutResult.Status.Should().Be(CheckoutStatus.Active);
        
        getExpiredCheckoutResult.Should().NotBeNull();
        getExpiredCheckoutResult.Status.Should().Be(CheckoutStatus.Expired);
        
    }
    
}