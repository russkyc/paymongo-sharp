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

using Paymongo.Sharp.Sources.Entities;

namespace paymongo_sharp.tests.IntegrationTests;

public class SourceApiTests
{
    private readonly IPaymongoClient _client;

    public SourceApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }

    [Theory]
    [InlineData(SourceType.GCash)]
    [InlineData(SourceType.GrabPay)]
    async Task CreateSource(SourceType type)
    {
        // Arrange
        Source source = new Source
        {
            Amount = 100000,
            Description = "New GcashPayment",
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
            Type = type,
            Currency = Currency.Php
        };

        // Act
        var sourceResult = await _client.Sources.CreateSourceAsync(source);

        // Assert
        sourceResult.Should().NotBeNull();
        sourceResult.Id.Should().NotBeNullOrEmpty();
        sourceResult.Description.Should().BeEquivalentTo(source.Description);
        sourceResult.Amount.Should().Be(source.Amount);
        sourceResult.Billing.Should().BeEquivalentTo(source.Billing);
        sourceResult.Redirect!.Success.Should().BeEquivalentTo(source.Redirect.Success);
        sourceResult.Redirect!.Failed.Should().BeEquivalentTo(source.Redirect.Failed);
        sourceResult.Redirect!.CheckoutUrl.Should().NotBeNullOrEmpty();
    }

    [Theory]
    [InlineData(SourceType.GCash)]
    [InlineData(SourceType.GrabPay)]
    async Task CreateAndRetrieveSource(SourceType type)
    {
        // Arrange
        Source source = new Source
        {
            Amount = 100000,
            Description = "New GcashPayment",
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
            Type = type,
            Currency = Currency.Php
        };

        // Act
        var sourceResult = await _client.Sources.CreateSourceAsync(source);
        var getSourceResult = await _client.Sources.RetrieveSourceAsync(sourceResult.Id);
        
        // Assert
        getSourceResult.Should().NotBeNull();
        getSourceResult.Should().BeEquivalentTo(sourceResult);
    }
}