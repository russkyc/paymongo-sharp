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

using Paymongo.Sharp.Features.Links.Contracts;

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
    async Task CreateAndRetrieveLink()
    {
        // Arrange
        Link link = new Link()
        {
            Data = new LinkData()
            {
                Attributes = new LinkAttributes()
                {
                    Description = "New Payment Link",
                    Amount = 100000,
                    Currency = Currency.Php,
                    Remarks = "Sample Remarks"
                }
            }
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);

        linkResult.Should().NotBeNull();
        var getLinkResult = await _client.Links.RetrieveLinkAsync(linkResult.Data.Id);

        // Assert
        getLinkResult.Should().BeEquivalentTo(linkResult);
    }

    [Fact]
    async Task CreateArchiveAndUnarchiveLink()
    {
        // Arrange
        Link link = new Link()
        {
            Data = new LinkData()
            {
                Attributes = new LinkAttributes()
                {
                    Description = "New Link",
                    ReferenceNumber = "61223292",
                    Amount = 100000,
                    Currency = Currency.Php
                }
            }
        };

        // Act
        var linkResult = await _client.Links.CreateLinkAsync(link);
        var getArchiveLinkResult = await _client.Links.ArchiveLinkAsync(linkResult.Data.Id);
        var getUnarchiveLinkResult = await _client.Links.UnarchiveLinkAsync(linkResult.Data.Id);

        // Assert
        getArchiveLinkResult.Should().NotBeNull();
        getArchiveLinkResult.Data.Attributes.Archived.Should().BeTrue();
        getUnarchiveLinkResult.Data.Attributes.Archived.Should().BeFalse();
    }
}