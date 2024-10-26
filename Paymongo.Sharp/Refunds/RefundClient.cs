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

using System.Threading.Tasks;
using Newtonsoft.Json;
using Paymongo.Sharp.Refunds.Entities;
using Paymongo.Sharp.Helpers;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Paymongo.Sharp.Refunds
{
    public class RefundClient
    {
        private const string Resource = "/refunds";
        private readonly RestClient _client;
    
        private readonly string _secretKey;

        public RefundClient(string baseUrl, string secretKey)
        {
            _client = new RestClient(new RestClientOptions(baseUrl));
            _secretKey = secretKey;
        }

        public async Task<Refund> CreateRefundAsync(Refund refund)
        {
        
            var data = new RefundRequestData
            {
                Data = new RefundRequestAttributes
                {
                    Attributes = refund
                }
            };

            var body = JsonConvert.SerializeObject(data);
        
            var request = RequestHelpers.Create(Resource,_secretKey,_secretKey, body);
            var response = await _client.PostAsync(request);

            return response.Content.ToRefund();
        }

        public async Task<Refund> RetrieveRefundAsync(string id)
        {
            var request = RequestHelpers.Create($"{Resource}/{id}",_secretKey,_secretKey);
            var response = await _client.GetAsync(request);
            
            return response.Content.ToRefund();
        }

        public async Task<IEnumerable<Refund>> ListAllRefundsAsync(string? paymentId = null, int limit = Int32.MaxValue, string? before = null, string? after = null)
        {
            List<string> parameters = new List<string>();

            if (paymentId != null)
            {
                parameters.Add($"data.attributes.payment_id={paymentId}");
            }
            if (limit != Int32.MaxValue)
            {
                parameters.Add($"data.attributes.limit={limit}");
            }
            if (before != null)
            {
                parameters.Add($"data.attributes.before={before}");
            }
            if (after != null)
            {
                parameters.Add($"data.attributes.after={after}");
            }

            var paramsCollection = $"?{string.Join("&", parameters)}";

            var request = RequestHelpers.Create($"{Resource}/{(parameters.Any() ? paramsCollection : string.Empty)}", _secretKey, _secretKey);
            var response = await _client.GetAsync(request);

            dynamic refundsResponse = JObject.Parse(response.Content!);
            string refundsData = refundsResponse.data.ToString();
            return refundsData.ToRefunds();
        }
    }
}