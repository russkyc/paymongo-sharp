// MIT License
// 
// Copyright (c) $CURRENT_YEAR$ Russell Camo (@russkyc)
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

using Paymongo.Sharp.PaymentMethods.Entities;
using PaymentMethod = Paymongo.Sharp.PaymentMethods.Entities.PaymentMethod;

namespace paymongo_sharp.tests.IntegrationTests;

public class PaymentMethodApiTests
{
    private readonly IPaymongoClient _client;

    public PaymentMethodApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }

    [Fact]
    async Task CreateAndRetrievePaymentMethod()
    {
        // Arrange
        var paymentMethod = new PaymentMethod
        {
            Type = PaymentMethodType.Card
        };

        // Act
        var paymentMethodResult = await _client.PaymentMethods.CreatePaymentMethodAsync(paymentMethod);
        var getPaymentMethodResult = await _client.PaymentMethods.RetrievePaymentMethodAsync(paymentMethod.Id);
        
        // Assert
        Assert.NotNull(paymentMethodResult);
        Assert.NotNull(getPaymentMethodResult);
        Assert.Equal(paymentMethod.Type,paymentMethodResult.Type);
        Assert.Equal(paymentMethodResult.Type,getPaymentMethodResult.Type);
        Assert.NotEmpty(paymentMethodResult.Id);
        Assert.NotEmpty(getPaymentMethodResult.Id);
    }
    
    [Fact]
    async Task CreateAndUpdatePaymentMethod()
    {
        // Arrange
        var paymentMethod = new PaymentMethod
        {
            Type = PaymentMethodType.Card
        };

        // Act
        var paymentMethodResult = await _client.PaymentMethods.CreatePaymentMethodAsync(paymentMethod);

        // Assert
        Assert.NotNull(paymentMethodResult);
        Assert.Equal(paymentMethod.Type,paymentMethodResult.Type);
        Assert.NotEmpty(paymentMethodResult.Id);
        
        paymentMethodResult.Type = PaymentMethodType.Card;
        paymentMethodResult.Cvc = 123;

        var updatedPaymentMethodResult = await _client.PaymentMethods.UpdatePaymentMethodAsync(paymentMethodResult);
        
        // Assert
        Assert.Equal(paymentMethodResult,updatedPaymentMethodResult);
        
    }

    [Fact]
    async Task RetrievePaymentMethods()
    {
        // Arrange and Act
        var paymentMethods = await _client.PaymentMethods.RetrievePaymentMethodsAsync();

        // Assert
        Assert.NotNull(paymentMethods);
    }
}