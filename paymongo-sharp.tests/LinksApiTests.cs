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

using Paymongo.Sharp;
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Interfaces;
using Paymongo.Sharp.Links.Entities;

namespace paymongo_sharp.tests;

public class LinksApiTests
{
    private readonly IPaymongoClient _client;

    public LinksApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }
    
    [Fact]
    async Task CreateLink()
    {
        // Arrange
        Link link = new Link
        {
            Description = "New Link",
            Amount = 100000,
            Currency = Currency.Php
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);

        // Assert
        Assert.NotNull(linkResult);
    }
    
    [Fact]
    async Task CreateAndRetrieveLink()
    {
        // Arrange
        Link link = new Link
        {
            Description = "New Link",
            Amount = 100000,
            Currency = Currency.Php
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);

        // Assert
        Assert.NotNull(linkResult);

        var getLinkResult = await _client.Links.RetrieveLinkAsync(linkResult.Id);
        
        Assert.NotNull(getLinkResult);
    }

}