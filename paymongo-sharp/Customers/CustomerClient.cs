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
using Paymongo.Sharp.Checkouts.Entities;
using Paymongo.Sharp.Customers.Entities;
using Paymongo.Sharp.Helpers;
using RestSharp;

namespace Paymongo.Sharp.Customers
{
    public class CustomerClient
    {
        private const string Resource = "/customers";
        private readonly RestClient _client;
    
        private readonly string _secretKey;

        public CustomerClient(string baseUrl, string secretKey)
        {
            _client = new RestClient(new RestClientOptions(baseUrl));
            _secretKey = secretKey;
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            var data = new CustomerRequestData
            {
                Data = new CustomerRequestAttributes
                {
                    Attributes = customer
                }
            };

            var body = JsonConvert.SerializeObject(data);
        
            var request = RequestHelpers.Create(Resource,_secretKey,_secretKey, body);
            var response = await _client.PostAsync(request);

            return response.Content.ToCustomer();
        }
        
        public async Task<bool> DeleteCustomerAsync(string id)
        {
            var request = RequestHelpers.Create($"{Resource}/{id}",_secretKey,_secretKey);
            var response = await _client.DeleteAsync(request);
            return response.Content.ToCustomerResultBool();
        }
    }
}