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

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Paymongo.Sharp.Features.Payments.Entities;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.Utilities;

namespace Paymongo.Sharp.Features.Payments
{
    public class PaymentClient
    {
        private const string Resource = "/payments";
        private readonly HttpClient _client;

        public PaymentClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            var data = payment.ToSchema();
            return await _client.SendRequestAsync<Payment>(HttpMethod.Post, Resource, data, content => content.ToPayment());
        }

        public async Task<Payment> RetrievePaymentAsync(string id)
        {
            return await _client.SendRequestAsync<Payment>(HttpMethod.Get, $"{Resource}/{id}", responseDeserializer: content => content.ToPayment());
        }

        public async Task<IEnumerable<Payment>> ListAllPaymentsAsync(int limit = int.MaxValue, string? before = null, string? after = null)
        {
            var parameters = new List<string>();
            if (limit != int.MaxValue)
                parameters.Add($"limit={limit}");
            if (before != null)
                parameters.Add($"before={before}");
            if (after != null)
                parameters.Add($"after={after}");
            var url = parameters.Any() ? $"{Resource}?{string.Join("&", parameters)}" : Resource;
            return await _client.SendRequestAsync<IEnumerable<Payment>>(HttpMethod.Get, url, responseDeserializer: content => content.ToPayments());
        }
    }
}