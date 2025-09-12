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

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Paymongo.Sharp.Features.WebHooks.Contracts;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.Utilities;

namespace Paymongo.Sharp.Features.WebHooks
{
    public class WebhooksClient
    {
        private const string Resource = "/webhooks";
        private readonly HttpClient _client;

        public WebhooksClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Webhook> CreateWebhookAsync(Webhook webhook)
        {
            return await _client.SendRequestAsync<Webhook>(HttpMethod.Post, Resource, webhook, content => content.ToWebHook());
        }
        
        public async Task<Webhook> RetrieveWebhookAsync(string id)
        {
            return await _client.SendRequestAsync<Webhook>(HttpMethod.Get, $"{Resource}/{id}", responseDeserializer: content => content.ToWebHook());
        }

        public async Task<IEnumerable<WebhookData>> ListWebhooksAsync()
        {
            return await _client.SendRequestAsync<IEnumerable<WebhookData>>(HttpMethod.Get, Resource, responseDeserializer: content => content.ToWebHookList());
        }
        
        public async Task<Webhook> UpdateWebhookAsync(Webhook webhook)
        {
            return await _client.SendRequestAsync<Webhook>(HttpMethod.Put, $"{Resource}/{webhook.Data.Id}", webhook, content => content.ToWebHook());
        }
        
        public async Task<Webhook> EnableWebhookAsync(string id)
        {
            return await _client.SendRequestAsync<Webhook>(HttpMethod.Post, $"{Resource}/{id}/enable", responseDeserializer: content => content.ToWebHook());
        }
        
        public async Task<Webhook> DisableWebhookAsync(string id)
        {
            return await _client.SendRequestAsync<Webhook>(HttpMethod.Post, $"{Resource}/{id}/disable", responseDeserializer: content => content.ToWebHook());
        }
    }
}