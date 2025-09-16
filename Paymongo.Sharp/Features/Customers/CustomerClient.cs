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
using Paymongo.Sharp.Features.Customers.Contracts;
using Paymongo.Sharp.Helpers;

namespace Paymongo.Sharp.Features.Customers
{
    public class CustomerClient
    {
        private const string Resource = "/customers";
        private readonly HttpClient _client;

        public CustomerClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer, string? idempotencyKey = null)
        {
            return await _client.SendRequestAsync<Customer>(HttpMethod.Post, Resource, customer, content => content.ToCustomer(), idempotencyKey);
        }

        public async Task<Customer> EditCustomerAsync(Customer customer)
        {
            return await _client.SendRequestAsync<Customer>(HttpMethod.Put, $"{Resource}/{customer.Data.Id}", customer, content => content.ToCustomer());
        }

        public async Task<IEnumerable<CustomerData>> RetrieveCustomerAsync(string email, string phoneNumber)
        {
            var url = $"{Resource}?email={email}&phone={phoneNumber}";
            return await _client.SendRequestAsync<IEnumerable<CustomerData>>(HttpMethod.Get, url, responseDeserializer: content => content.ToCustomers());
        }

        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var url = $"{Resource}/{id}";
            return await _client.SendRequestAsync(HttpMethod.Delete, url, responseDeserializer: content => content.ToCustomerResultBool());
        }
    }
}