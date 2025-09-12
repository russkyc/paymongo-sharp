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

using Paymongo.Sharp.Features.Payments.Contracts;
using Paymongo.Sharp.Features.Sources.Entities;

namespace Paymongo.Sharp.Tests.Integration;

public class PaymentApiTests
{
    private readonly IPaymongoClient _client;

    public PaymentApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }

    [Fact]
    async Task CreatePayment()
    {
        // Arrange
        
        // A payment requires a source, so we need to create a source first
        Source source = new Source
        {
            Amount = 10000,
            Description = $"New GCash Source",
            Redirect = new Redirect
            {
                Success = "http://127.0.0.1",
                Failed = "http://127.0.0.1"
            },
            Type = SourceType.GCash,
            Currency = Currency.Php
        };

        // Act
        var sourceResult = await _client.Sources.CreateSourceAsync(source);
        
        Payment payment = new Payment
        {
            Data = new PaymentData()
            {
                Attributes = new PaymentAttributes()
                {
                    Description = "New Payment",
                    Amount = 10000,
                    Fee = 200,
                    NetAmount = 9800,
                    Currency = Currency.Php,
                    Billing = new Billing(),
                    Source = new PaymentSource()
                    {
                        Id = sourceResult.Id,
                        Type = "source"
                    }
                }
            }
        };

        var paymentResult = await _client.Payments.CreatePaymentAsync(payment);

        // Assert
        Assert.NotNull(paymentResult);
    }

    [Fact]
    async Task RetrievePayment()
    {
        // ! Will remain this way until CreatePayment test gets fixed, sorry
        var paymentResult = await _client.Payments.RetrievePaymentAsync("pay_Lj5aRPSU9p6ozZdQxLuwjpiT");
        Assert.NotNull(paymentResult);
    }

    [Fact]
    async Task ListAllPayments()
    {
        var paymentsResult = await _client.Payments.ListAllPaymentsAsync();
        Assert.NotEmpty(paymentsResult);
    }

}