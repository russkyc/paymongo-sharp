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
using System.Net.Http;
using System.Threading.Tasks;
using Paymongo.Sharp.Features.PaymentMethods.Entities;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.Utilities;

namespace Paymongo.Sharp.Features.PaymentMethods
{
    public class PaymentMethodsClient
    {
        private const string Resource = "/payment_methods";
        private const string MerchantResource = "/merchants/capabilities/payment_methods";
        private readonly HttpClient _client;

        public PaymentMethodsClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<PaymentMethod> CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            return await _client.SendRequestAsync<PaymentMethod>(HttpMethod.Post, Resource, paymentMethod, content => content.ToPaymentMethod());
        }

        public async Task<PaymentMethod> UpdatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            // Details cannot be updated
            paymentMethod.Data.Attributes.Details = null;
            return await _client.SendRequestAsync<PaymentMethod>(HttpMethod.Put, $"{Resource}/{paymentMethod.Data.Id}", paymentMethod, content => content.ToPaymentMethod());
        }

        public async Task<PaymentMethod> RetrievePaymentMethodAsync(string id)
        {
            return await _client.SendRequestAsync<PaymentMethod>(HttpMethod.Get, $"{Resource}/{id}", responseDeserializer: content => content.ToPaymentMethod());
        }

        public async Task<IEnumerable<PaymentMethod>> RetrievePaymentMethodsAsync()
        {
            return await _client.SendRequestAsync<IEnumerable<PaymentMethod>>(HttpMethod.Get, MerchantResource, responseDeserializer: content => content.ToPaymentMethods());
        }

    }
}