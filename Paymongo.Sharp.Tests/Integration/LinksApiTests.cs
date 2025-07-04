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

using Paymongo.Sharp.Features.Links.Entities;

namespace Paymongo.Sharp.Tests.Integration;

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
            Description = "New Payment Link",
            Amount = 10000,
            Currency = Currency.Php,
            Remarks = "Sample Remarks"
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);

        // Assert
        linkResult.Should().NotBeNull();
        linkResult.Id.Should().NotBeNull();
        linkResult.ReferenceNumber.Should().NotBeNullOrEmpty();
        linkResult.CheckoutUrl.Should().NotBeNullOrEmpty();
        linkResult.Description.Should().BeEquivalentTo(link.Description);
        linkResult.Remarks.Should().BeEquivalentTo(link.Remarks);
        linkResult.Amount.Should().Be(link.Amount);
        linkResult.Currency.Should().Be(link.Currency);
        linkResult.Status.Should().Be(LinkStatus.Unpaid);
    }
    
    [Fact]
    async Task CreateAndRetrieveLink()
    {
        // Arrange
        Link link = new Link
        {
            Description = "New Payment Link",
            Amount = 100000,
            Currency = Currency.Php,
            Remarks = "Sample Remarks"
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);
        var getLinkResult = await _client.Links.RetrieveLinkAsync(linkResult.Id);
        
        // Assert
        getLinkResult.Should().NotBeNull();
        getLinkResult.Should().BeEquivalentTo(linkResult);

    }
    
    [Fact]
    async Task CreateAndRetrieveLinkByReferenceNumber()
    {
        // Arrange
        Link link = new Link
        {
            Description = "New Link",
            Amount = 100000,
            Remarks = "Sample Remarks",
            Currency = Currency.Php
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);
        var getLinkResult = await _client.Links.GetLinkByReferenceNumberAsync(linkResult.ReferenceNumber);

        // Assert
        getLinkResult.Should().NotBeNull();
        getLinkResult.Should().BeEquivalentTo(linkResult);
    }
    
    [Fact]
    async Task CreateArchiveAndUnarchiveLink()
    {
        // Arrange
        Link link = new Link
        {
            Description = "New Link",
            ReferenceNumber = "61223292",
            Amount = 100000,
            Currency = Currency.Php
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);
        var getArchiveLinkResult = await _client.Links.ArchiveLinkAsync(linkResult.Id);
        var getUnarchiveLinkResult = await _client.Links.UnarchiveLinkAsync(linkResult.Id);
        
        // Assert
        getArchiveLinkResult.Should().NotBeNull();
        getArchiveLinkResult.Archived.Should().BeTrue();
        getUnarchiveLinkResult.Archived.Should().BeFalse();

    }

}