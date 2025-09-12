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
using Paymongo.Sharp.Features.Refunds.Contracts;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.Utilities;

namespace Paymongo.Sharp.Features.Refunds
{
    public class RefundClient
    {
        private const string Resource = "/refunds";
        private readonly HttpClient _client;

        public RefundClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Refund> CreateRefundAsync(Refund refund)
        {
            return await _client.SendRequestAsync<Refund>(HttpMethod.Post, Resource, refund, content => content.ToRefund());
        }

        public async Task<Refund> RetrieveRefundAsync(string id)
        {
            return await _client.SendRequestAsync<Refund>(HttpMethod.Get, $"{Resource}/{id}", responseDeserializer: content => content.ToRefund());
        }

        public async Task<IEnumerable<RefundData>> ListAllRefundsAsync(string? paymentId = null, int limit = int.MaxValue, string? before = null, string? after = null)
        {
            var parameters = new List<string>();
            if (paymentId != null)
                parameters.Add($"payment_id={paymentId}");
            if (limit != int.MaxValue)
                parameters.Add($"limit={limit}");
            if (before != null)
                parameters.Add($"before={before}");
            if (after != null)
                parameters.Add($"after={after}");
            var url = parameters.Any() ? $"{Resource}?{string.Join("&", parameters)}" : Resource;
            return await _client.SendRequestAsync<IEnumerable<RefundData>>(HttpMethod.Get, url, responseDeserializer: content => content?.ToRefunds() ?? Enumerable.Empty<RefundData>());
        }
    }
}