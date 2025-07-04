// MIT License
// 
// Copyright (c) $CURRENT_YEAR$ Russell Camo (@russkyc)
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
using System.Threading.Tasks;
using Newtonsoft.Json;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.PaymentMethods.Entities;
using Paymongo.Sharp.Utilities;
using RestSharp;

namespace Paymongo.Sharp.PaymentMethods
{
    public class PaymentMethodsClient
    {
        private const string Resource = "/payment_methods";
        private const string MerchantResource = "/merchants/capabilities/payment_methods";
        private readonly RestClient _client;
    
        private readonly string _secretKey;

        public PaymentMethodsClient(string baseUrl, string secretKey)
        {
            _client = new RestClient(new RestClientOptions(baseUrl));
            _secretKey = secretKey;
        }

        public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            var data = paymentMethod.ToSchema();

            var body = JsonConvert.SerializeObject(data);
        
            var request = RequestHelpers.Create(Resource,_secretKey,_secretKey, body);
            var response = await _client.PostAsync(request);

            return response.Content.ToPaymentMethod();
        }
        
        public async Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            var data = paymentMethod.ToSchema();

            var body = JsonConvert.SerializeObject(data);
        
            var request = RequestHelpers.Create($"{Resource}/{paymentMethod.Id}",_secretKey,_secretKey, body);
            var response = await _client.PutAsync(request);

            return response.Content.ToPaymentMethod();
        }

        public async Task<PaymentMethod> RetrievePaymentMethodAsync(string id)
        {
            var request = RequestHelpers.Create($"{Resource}/{id}",_secretKey,_secretKey);
            var response = await _client.GetAsync(request);
            return response.Content.ToPaymentMethod();
        }
        
        public async Task<IEnumerable<PaymentMethod>> RetrievePaymentMethodsAsync()
        {
            var request = RequestHelpers.Create(MerchantResource,_secretKey,_secretKey);
            var response = await _client.GetAsync(request);
            return response.Content.ToPaymentMethods();
        }

    }
}