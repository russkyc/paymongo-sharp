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

using Paymongo.Sharp.Features.CardInstallments.Contracts;
using Paymongo.Sharp.Features.PaymentIntents.Contracts;
using Paymongo.Sharp.Features.PaymentMethods.Contracts;
using Paymongo.Sharp.Tests.Utils;
using PaymentMethodOption = Paymongo.Sharp.Features.PaymentIntents.Contracts.PaymentMethodOption;
using PaymentMethodType = Paymongo.Sharp.Features.PaymentMethods.Contracts.PaymentMethod;

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
            Data = new PaymentIntentData()
            {
                Attributes = new PaymentIntentAttributes()
                {
                    Amount = 10000,
                    Currency = Currency.Php,
                    PaymentMethodAllowed =
                    [
                        Core.Enums.PaymentMethodType.Card,
                        Core.Enums.PaymentMethodType.Paymaya,
                        Core.Enums.PaymentMethodType.GCash
                    ],
                    PaymentMethodOptions = new PaymentMethodOption()
                    {
                        Card = new Card()
                    }
                }
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
            Data = new PaymentIntentData()
            {
                Attributes = new PaymentIntentAttributes()
                {
                    Amount = 10000,
                    Currency = Currency.Php,
                    PaymentMethodAllowed =
                    [
                        Core.Enums.PaymentMethodType.Card,
                        Core.Enums.PaymentMethodType.Paymaya,
                        Core.Enums.PaymentMethodType.GCash
                    ],
                    PaymentMethodOptions = new PaymentMethodOption()
                    {
                        Card = new Card()
                    }
                }
            }
        };
        
        // Act
        var paymentIntentResult = await _client.PaymentIntents.CreatePaymentIntentAsync(paymentIntent);

        // Act
        var getPaymentIntentResult = await _client.PaymentIntents.RetrievePaymentIntentAsync(paymentIntentResult.Data.Id);
        
        // Assert
        Assert.NotNull(getPaymentIntentResult);
    }
    
    [Fact]
    async Task PaymentIntentWorkflow()
    {
        // Arrange
        var paymentMethod = new PaymentMethodType
        {
            Data = new PaymentMethodData()
            {
                Attributes = new PaymentMethodAttributes()
                {
                    Type = Core.Enums.PaymentMethodType.GCash,
                    Billing = DataFakers.GenerateBilling()
                }
            }
        };

        // Act
        var paymentMethodResult = await _client.PaymentMethods.CreatePaymentMethodAsync(paymentMethod);
        
        PaymentIntent paymentIntent = new PaymentIntent
        {
            Data = new PaymentIntentData()
            {
                Attributes = new PaymentIntentAttributes()
                {
                    Amount = 10000,
                    Currency = Currency.Php,
                    PaymentMethodAllowed =
                    [
                        Core.Enums.PaymentMethodType.Card,
                        Core.Enums.PaymentMethodType.Paymaya,
                        Core.Enums.PaymentMethodType.GCash
                    ],
                    PaymentMethodOptions = new PaymentMethodOption()
                    {
                        Card = new Card()
                    }
                }
            }
        };
        
        // Act
        var createdPaymentIntent = await _client.PaymentIntents.CreatePaymentIntentAsync(paymentIntent);

        PaymentIntentAttachment paymentIntentAttachment = new PaymentIntentAttachment
        {
            Data = new PaymentIntentAttachmentData()
            {
                Attributes = new PaymentIntentAttachmentAttributes()
                {
                    PaymentMethod = paymentMethodResult.Data.Id,
                    ReturnUrl = "https://google.com"
                }
            }
        };
        
        // Act
        var paymentIntentResult = await _client.PaymentIntents.AttachToPaymentIntentAsync(createdPaymentIntent.Data.Id, paymentIntentAttachment);
        
        // Assert
        Assert.NotNull(paymentIntentResult);
    }
}
