// MIT License
// 
// Copyright (c) 2025 Russell Camo (@russkyc)
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

using Paymongo.Sharp.Features.CardInstallments.Entities;
using Paymongo.Sharp.Features.PaymentIntents.Entities;

namespace Paymongo.Sharp.Tests.Integration;

public class PaymentIntentApiTests
{
    private readonly IPaymongoClient _client;

    public PaymentIntentApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }

    [Fact]
    async Task CreatePaymentIntent()
    {
        // Arrange
        PaymentIntent paymentIntent = new PaymentIntent
        {
            Amount = 10000,
            Currency = Currency.Php,
            PaymentMethodAllowed =
            [
                PaymentMethod.Card,
                PaymentMethod.Paymaya
            ],
            PaymentMethodOptions = new PaymentMethodOption()
            {
                Card = new Card()
            }
        };
        
        // Act
        var paymentIntentResult = await _client.PaymentIntents.CreatePaymentIntentAsync(paymentIntent);
        
        // Assert
        Assert.NotNull(paymentIntentResult);
    }
    
    [Fact]
    async Task RetrievePaymentIntent()
    {
        // Arrange
        PaymentIntent paymentIntent = new PaymentIntent
        {
            Amount = 10000,
            Currency = Currency.Php,
            PaymentMethodAllowed =
            [
                PaymentMethod.Card,
                PaymentMethod.Paymaya
            ],
            PaymentMethodOptions = new PaymentMethodOption()
            {
                Card = new Card()
            }
        };
        
        // Act
        var paymentIntentResult = await _client.PaymentIntents.CreatePaymentIntentAsync(paymentIntent);

        // Act
        var getPaymentIntentResult = await _client.PaymentIntents.RetrievePaymentIntentAsync(paymentIntentResult.Id);
        
        // Assert
        Assert.NotNull(getPaymentIntentResult);
    }
    
    [Fact]
    async Task AttachToPaymentIntent()
    {
        // Arrange
        const string paymentIntentId = "pi_WENqK7d5L3XN9YQzEt39B3oF";
        
        PaymentIntentAttachment paymentIntentAttachment = new PaymentIntentAttachment
        {
            PaymentMethod = PaymentMethod.Card,
            ReturnUrl = "https://google.com"
        };
        
        // Act
        var paymentIntentResult = await _client.PaymentIntents.AttachToPaymentIntentAsync(paymentIntentId, paymentIntentAttachment);
        
        // Assert
        Assert.NotNull(paymentIntentResult);
    }
}
