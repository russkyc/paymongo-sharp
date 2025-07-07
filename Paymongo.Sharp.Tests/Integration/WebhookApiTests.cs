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

using Paymongo.Sharp.Features.WebHooks.Entities;

namespace Paymongo.Sharp.Tests.Integration;

public class WebhookApiTests
{
    private readonly IPaymongoClient _client;
    private const string TestWebhookUrl = "https://www.google.com";
    private static readonly string[] TestEvents = ["source.chargeable", "payment.paid"];

    public WebhookApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }

    [Fact]
    public async Task CreateWebhook()
    {
        // Arrange
        var webhook = new Webhook
        {
            Url = TestWebhookUrl,
            Events = TestEvents
        };

        // Act
            var created = await _client.Webhooks.CreateWebhookAsync(webhook);

        // Assert
        Assert.NotNull(created);
        Assert.Equal(TestWebhookUrl, created.Url);
        Assert.NotNull(created.Id);

        // Cleanup
        await _client.Webhooks.DisableWebhookAsync(created.Id);
    }

    [Fact]
    public async Task CreateAndRetrieveWebhook()
    {
        // Arrange
        var webhook = new Webhook
        {
            Url = TestWebhookUrl,
            Events = TestEvents
        };
        var created = await _client.Webhooks.CreateWebhookAsync(webhook);

        // Act
        var retrieved = await _client.Webhooks.RetrieveWebhookAsync(created.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(created.Id, retrieved.Id);
        Assert.Equal(TestWebhookUrl, retrieved.Url);

        // Cleanup
        await _client.Webhooks.DisableWebhookAsync(created.Id);
    }
    
    [Fact]
    public async Task ListWebhooks()
    {
        // Act
        var webhooks = await _client.Webhooks.ListWebhooksAsync();
        var collection = webhooks as Webhook[] ?? webhooks.ToArray();
        
        // Assert
        Assert.NotNull(webhooks);
        Assert.NotEmpty(collection);
    }

    [Fact]
    public async Task CreateAndUpdateWebhook()
    {
        // Arrange
        var webhook = new Webhook
        {
            Url = TestWebhookUrl,
            Events = TestEvents
        };
        var created = await _client.Webhooks.CreateWebhookAsync(webhook);
        var updatedUrl = TestWebhookUrl + "/updated";
        created.Url = updatedUrl;

        // Act
        var updated = await _client.Webhooks.UpdateWebhookAsync(created);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(updatedUrl, updated.Url);

        // Cleanup
        await _client.Webhooks.DisableWebhookAsync(created.Id);
    }

    [Fact]
    public async Task CreateAndEnableWebhook()
    {
        // Arrange
        var webhook = new Webhook
        {
            Url = TestWebhookUrl,
            Events = TestEvents
        };
        var created = await _client.Webhooks.CreateWebhookAsync(webhook);

        // Act
        var enabled = await _client.Webhooks.EnableWebhookAsync(created.Id);

        // Assert
        Assert.NotNull(enabled);
        Assert.Equal(WebhookStatus.Enabled, enabled.Status);

        // Cleanup
        await _client.Webhooks.DisableWebhookAsync(created.Id);
    }

    [Fact]
    public async Task CreateAndDisableWebhook()
    {
        // Arrange
        var webhook = new Webhook
        {
            Url = TestWebhookUrl,
            Events = TestEvents
        };
        var created = await _client.Webhooks.CreateWebhookAsync(webhook);

        // Act
        var disabled = await _client.Webhooks.DisableWebhookAsync(created.Id);

        // Assert
        Assert.NotNull(disabled);
        Assert.Equal(WebhookStatus.Disabled, disabled.Status);
    }
}