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
using Paymongo.Sharp.Features.Checkouts;
using Paymongo.Sharp.Features.Customers;
using Paymongo.Sharp.Features.Links;
using Paymongo.Sharp.Features.PaymentMethods;
using Paymongo.Sharp.Features.Payments;
using Paymongo.Sharp.Features.QrPh;
using Paymongo.Sharp.Features.Refunds;
using Paymongo.Sharp.Features.Sources;
using Paymongo.Sharp.Features.WebHooks;
using Paymongo.Sharp.Helpers;
using Paymongo.Sharp.Interfaces;

namespace Paymongo.Sharp
{
    public class PaymongoClient : IPaymongoClient
    {
        private const string ApiEndpoint = "https://api.paymongo.com/v1";
        private readonly HttpClient _httpClient;
        
        public PaymongoClient(string apiKey)
        {
            // Initialize HttpClient
            _httpClient = new HttpClient
            {
                BaseAddress = new System.Uri(ApiEndpoint),
                DefaultRequestHeaders =
                {
                    { "Accept", "*/*" },
                    { "User-Agent", "Paymongo.Sharp Client" },
                    { "Authorization", $"Basic {apiKey.Encode()}"}
                }
            };
            
            // Init internal clients
            Checkouts = new CheckoutClient(_httpClient);
            Payments = new PaymentClient(_httpClient);
            Links = new LinksClient(_httpClient);
            Sources = new SourceClient(_httpClient);
            Customers = new CustomerClient(_httpClient);
            PaymentMethods = new PaymentMethodsClient(_httpClient);
            Refunds = new RefundClient(_httpClient);
            QrPh = new QrPhClient(_httpClient);
            Webhooks = new WebhooksClient(_httpClient);
        }

        public CheckoutClient Checkouts { get; }
        public PaymentClient Payments { get; }
        public LinksClient Links { get; }
        public SourceClient Sources { get; }
        public CustomerClient Customers { get; }
        public PaymentMethodsClient PaymentMethods { get; }
        public RefundClient Refunds { get; }
        public QrPhClient QrPh { get; }
        public WebhooksClient Webhooks { get; }
    }
}