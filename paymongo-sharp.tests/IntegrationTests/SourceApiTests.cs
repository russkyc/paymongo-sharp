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

using DotNetEnv;
using Paymongo.Sharp;
using Paymongo.Sharp.Core.Entities;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Interfaces;
using Paymongo.Sharp.Sources.Entities;
using Xunit;

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

    [Fact]
    async Task CreateSource()
    {
        // Arrange
        Source source = new Source
        {
            Amount = 100000,
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
        var sourceResult = await _client.Sources.CreateSourceAsync(source);

        // Assert
        Assert.NotNull(sourceResult);
    }

    [Fact]
    async Task CreateAndRetrieveSource()
    {
        // Arrange
        Source source = new Source
        {
            Amount = 100000,
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
        var sourceResult = await _client.Sources.CreateSourceAsync(source);

        // Assert
        Assert.NotNull(sourceResult);

        var getSourceResult = await _client.Sources.RetrieveSourceAsync(sourceResult.Id);
        
        // Assert
        Assert.NotNull(getSourceResult);
    }
}