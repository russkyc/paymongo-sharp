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

using Paymongo.Sharp.Checkouts;
using Paymongo.Sharp.Customers;
using Paymongo.Sharp.Interfaces;
using Paymongo.Sharp.Links;
using Paymongo.Sharp.Payments;
using Paymongo.Sharp.Sources;

namespace Paymongo.Sharp
{
    public class PaymongoClient : IPaymongoClient
    {
        private const string ApiEndpoint = "https://api.paymongo.com/v1";
        
        public PaymongoClient(string secretKey)
        {
            // Init internal clients
            Checkouts = new CheckoutClient(ApiEndpoint, secretKey);
            Payments = new PaymentClient(ApiEndpoint, secretKey);
            Links = new LinksClient(ApiEndpoint, secretKey);
            Sources = new SourceClient(ApiEndpoint, secretKey);
            Customers = new CustomerClient(ApiEndpoint, secretKey);
        }

        public CheckoutClient Checkouts { get; }
        public PaymentClient Payments { get; }
        public LinksClient Links { get; }
        public SourceClient Sources { get; set; }
        public CustomerClient Customers { get; set; }
    }
}