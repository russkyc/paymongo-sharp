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
using Paymongo.Sharp.Core.Enums;
using Paymongo.Sharp.Helpers;
using RestSharp;

namespace Paymongo.Sharp.Checkouts;

public class CheckoutClient
{
    private const string Resource = "/checkout_sessions";
    private readonly RestClient _client;
    
    private readonly string _secretKey;

    public CheckoutClient(string baseUrl, string secretKey)
    {
        _client = new RestClient(new RestClientOptions(baseUrl));
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
        
        var request = RequestHelpers.Create(Resource,_secretKey,_secretKey, body);
        var response = await _client.PostAsync(request);

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }

        var requestData = JsonConvert.DeserializeObject<CheckoutRequestData>(response.Content!)!;
        var checkoutResult = requestData.Data!.Attributes;

        if (checkoutResult is null)
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }
        
        // Checkout doesn't have an id field, so we take that from the parent
        checkoutResult.Id = requestData.Data.Id;
        
        // Unix timestamp doesn't account for daylight savings, so we adjust it here
        checkoutResult.CreatedAt = checkoutResult.CreatedAt.ToLocalDateTime();
        checkoutResult.UpdatedAt = checkoutResult.UpdatedAt.ToLocalDateTime();

        return checkoutResult;
    }

    public async Task<Checkout> RetrieveCheckoutAsync(string id)
    {
        var request = RequestHelpers.Create($"{Resource}/{id}",_secretKey,_secretKey);
        var response = await _client.GetAsync(request);

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }

        var requestData = JsonConvert.DeserializeObject<CheckoutRequestData>(response.Content!)!;
        var checkoutResult = requestData.Data!.Attributes;
        
        if (checkoutResult is null)
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }

        // Checkout doesn't have an id field, so we take that from the parent
        checkoutResult.Id = requestData.Data.Id;
        
        // Unix timestamp doesn't account for daylight savings, so we adjust it here
        checkoutResult.CreatedAt = checkoutResult.CreatedAt.ToLocalDateTime();
        checkoutResult.UpdatedAt = checkoutResult.UpdatedAt.ToLocalDateTime();
        
        return checkoutResult;
    }
    
    public async Task<Checkout> ExpireCheckoutAsync(string id)
    {
        var request = RequestHelpers.Create($"{Resource}/{id}/expire",_secretKey,_secretKey);
        var response = await _client.PostAsync(request);

        if (string.IsNullOrWhiteSpace(response.Content))
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }

        var requestData = JsonConvert.DeserializeObject<CheckoutRequestData>(response.Content!)!;
        var checkoutResult = requestData.Data!.Attributes;
        
        if (checkoutResult is null)
        {
            return new Checkout
            {
                Status = CheckoutStatus.Expired
            };
        }

        // Checkout doesn't have an id field, so we take that from the parent
        checkoutResult.Id = requestData.Data.Id;
        
        // Unix timestamp doesn't account for daylight savings, so we adjust it here
        checkoutResult.CreatedAt = checkoutResult.CreatedAt.ToLocalDateTime();
        checkoutResult.UpdatedAt = checkoutResult.UpdatedAt.ToLocalDateTime();

        return checkoutResult;
    }
}