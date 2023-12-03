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

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Paymongo.Sharp.Customers.Entities
{
    public class Customer
    {
        [JsonProperty("id",
            NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        
        [JsonProperty("organization_id",
            NullValueHandling = NullValueHandling.Ignore)]
        public string OrganizationId { get; set; }
        
        [JsonProperty("default_payment_method_id",
            NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultPaymentMethodId { get; set; }
        
        [JsonProperty("first_name",
            NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }
        
        [JsonProperty("last_name",
            NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }
        
        [JsonProperty("phone",
            NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }
        
        [JsonProperty("email",
            NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        
        [JsonProperty("live_mode",
            NullValueHandling = NullValueHandling.Ignore)]
        public bool LiveMode { get; set; }
        
        [JsonProperty("created_at",
            NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }
        
        [JsonProperty("updated_at",
            NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }
        
        [JsonProperty("payment_methods",
            NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<CustomerPaymentMethod> PaymentMethods { get; set; }
    }
}