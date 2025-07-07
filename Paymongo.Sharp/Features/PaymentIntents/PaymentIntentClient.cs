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

using System.Net.Http;
using System.Threading.Tasks;
using Paymongo.Sharp.Features.PaymentIntents.Entities;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.Utilities;

namespace Paymongo.Sharp.Features.PaymentIntents
{
    public class PaymentIntentClient
    {
        private const string Resource = "/payment_intents";
        private readonly HttpClient _client;

        public PaymentIntentClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<PaymentIntent> CreatePaymentIntentAsync(PaymentIntent paymentIntent)
        {
            var data = paymentIntent.ToSchema();
            return await _client.SendRequestAsync<PaymentIntent>(HttpMethod.Post, Resource, data, content => content.ToPaymentIntent());
        }

        public async Task<PaymentIntent> RetrievePaymentIntentAsync(string id)
        {
            return await _client.SendRequestAsync<PaymentIntent>(HttpMethod.Get, $"{Resource}/{id}", responseDeserializer: content => content.ToPaymentIntent());
        }

        public async Task<PaymentIntent> AttachToPaymentIntentAsync(string id, PaymentIntentAttachment paymentIntentAttachment)
        {
            var data = paymentIntentAttachment.ToSchema();
            return await _client.SendRequestAsync<PaymentIntent>(HttpMethod.Post, $"{Resource}/{id}/attach", data, content => content.ToPaymentIntent());
        }
    }
}