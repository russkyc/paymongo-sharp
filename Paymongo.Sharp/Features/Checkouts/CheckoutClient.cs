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

using System.Net.Http;
using System.Threading.Tasks;
using Paymongo.Sharp.Features.Checkouts.Entities;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.Utilities;

namespace Paymongo.Sharp.Features.Checkouts
{
    public class CheckoutClient
    {
        private const string Resource = "/checkout_sessions";
        private readonly HttpClient _client;

        public CheckoutClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Checkout> CreateCheckoutAsync(Checkout checkout)
        {
            var data = checkout.ToSchema();
            return await _client.SendRequestAsync<Checkout>(HttpMethod.Post, Resource, data, content => content.ToCheckout());
        }

        public async Task<Checkout> RetrieveCheckoutAsync(string id)
        {
            return await _client.SendRequestAsync<Checkout>(HttpMethod.Get, $"{Resource}/{id}", responseDeserializer: content => content.ToCheckout());
        }
    
        public async Task<Checkout> ExpireCheckoutAsync(string id)
        {
            return await _client.SendRequestAsync<Checkout>(HttpMethod.Post, $"{Resource}/{id}/expire", responseDeserializer: content => content.ToCheckout());
        }
    }
}