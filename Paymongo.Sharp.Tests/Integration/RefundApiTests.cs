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

using Paymongo.Sharp.Features.Refunds.Entities;
using Paymongo.Sharp.Features.Payments.Entities;

namespace Paymongo.Sharp.Tests.Integration;

public class RefundApiTests
{
    private readonly IPaymongoClient _client;

    public RefundApiTests()
    {
        Env.TraversePath().Load();
        var secretKey = Env.GetString("SECRET_KEY");
        _client = new PaymongoClient(secretKey);
    }

    [Fact]
    async Task CreateRefund()
    {
        // Arrange: create a payment first (minimal required fields)
        var payment = new Payment
        {
            Amount = 10000,
            Currency = Currency.Php,
            Source = new PaymentSource
            {
                Id = "src_test_12345678", // Replace with a valid source id for real test
                Type = "soutce"
            }
        };
        var paymentResult = await _client.Payments.CreatePaymentAsync(payment);

        // Act: create a refund for the payment
        var refund = new Refund
        {
            Amount = 10000,
            PaymentId = paymentResult.Id,
            Currency = Currency.Php,
            Notes = "Test refund"
        };
        var refundResult = await _client.Refunds.CreateRefundAsync(refund);

        // Assert
        refundResult.Should().NotBeNull();
        refundResult.Id.Should().NotBeNullOrEmpty();
        refundResult.Amount.Should().Be(refund.Amount);
        refundResult.PaymentId.Should().Be(paymentResult.Id);
        refundResult.Currency.Should().Be(refund.Currency);
    }

    [Fact]
    async Task RetrieveRefund()
    {
        // Arrange: create a payment and refund first
        var payment = new Payment
        {
            Amount = 10000,
            Currency = Currency.Php,
            Source = new PaymentSource
            {
                Id = "src_test_12345678", // Replace with a valid source id for real test
                Type = "source"
            }
        };
        var paymentResult = await _client.Payments.CreatePaymentAsync(payment);
        var refund = new Refund
        {
            Amount = 10000,
            PaymentId = paymentResult.Id,
            Currency = Currency.Php
        };
        var refundResult = await _client.Refunds.CreateRefundAsync(refund);

        // Act
        var getRefundResult = await _client.Refunds.RetrieveRefundAsync(refundResult.Id);

        // Assert
        getRefundResult.Should().NotBeNull();
        getRefundResult.Id.Should().Be(refundResult.Id);
        getRefundResult.Amount.Should().Be(refund.Amount);
    }

    [Fact]
    async Task ListAllRefunds()
    {
        // Arrange: create a payment and refund first
        var payment = new Payment
        {
            Amount = 10000,
            Currency = Currency.Php,
            Source = new PaymentSource
            {
                Id = "src_test_12345678", // Replace with a valid source id for real test
                Type = "source"
            }
        };
        var paymentResult = await _client.Payments.CreatePaymentAsync(payment);
        var refund = new Refund
        {
            Amount = 10000,
            PaymentId = paymentResult.Id,
            Currency = Currency.Php
        };
        await _client.Refunds.CreateRefundAsync(refund);

        // Act
        var refunds = await _client.Refunds.ListAllRefundsAsync(paymentId: paymentResult.Id, limit: 10);

        // Assert
        refunds.Should().NotBeNull();
        refunds.Should().Contain(r => r.PaymentId == paymentResult.Id);
    }
}

