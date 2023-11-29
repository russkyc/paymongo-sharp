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
using paymongo_sharp.Checkouts.Entities;
using paymongo_sharp.Core.Enums;
using paymongo_sharp.Helpers;
using RestSharp;

namespace paymongo_sharp.Checkouts;

public class CheckoutClient
{
    private readonly RestClient _client;
    
    private readonly string _publicKey;
    private readonly string _secretKey;

    public CheckoutClient(string baseUrl, string publicKey, string secretKey)
    {
        _client = new RestClient(new RestClientOptions(baseUrl));
        _publicKey = publicKey;
        _secretKey = secretKey;
    }

    public async Task<Checkout> CreateCheckoutAsync(Checkout checkout)
    {
        
        var data = new CheckoutRequestData
        {
            Data = new CheckoutRequestAttributes
            {
                Attributes = checkout
            }
        };

        var body = JsonConvert.SerializeObject(data);
        
        var request = RequestHelpers.Create("/checkout_sessions",_secretKey,_secretKey, body);

        var restResult = await _client.PostAsync(request);

        if (string.IsNullOrWhiteSpace(restResult.Content))
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }

        var requestData = JsonConvert.DeserializeObject<CheckoutRequestData>(restResult.Content!)!;

        var checkoutResult = requestData.Data.Attributes;
        checkoutResult.Id = requestData.Data.Id;
        
        return checkoutResult;
    }

    public async Task<Checkout> RetrieveCheckoutAsync(string id)
    {
        var request = RequestHelpers.Create($"/checkout_sessions/{id}",_secretKey,_secretKey);

        var restResult = await _client.GetAsync(request);

        if (string.IsNullOrWhiteSpace(restResult.Content))
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }

        var requestData = JsonConvert.DeserializeObject<CheckoutRequestData>(restResult.Content!)!;

        var checkoutResult = requestData.Data.Attributes;
        checkoutResult.Id = requestData.Data.Id;
        
        return checkoutResult;
    }
}