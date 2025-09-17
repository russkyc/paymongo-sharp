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

namespace Paymongo.Sharp.Tests.Integration;

public class CardInstallmentsApiTests
{
    private readonly IPaymongoClient _client;

    public CardInstallmentsApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }

    [Theory]
    [InlineData(10000)]
    [InlineData(50000)]
    [InlineData(100000)]
    [InlineData(200000)]
    async Task GetCardInstallments(int amount)
    {
        // Arrange and Act
        var result = await _client.CardInstallments.ListInstallmentPlansAsync(amount);

        // Assert
        var installmentPlans = result as Plan[] ?? result.ToArray();
        
        installmentPlans.Should().NotBeNull();
        installmentPlans.Should().NotBeEmpty();
    }
}