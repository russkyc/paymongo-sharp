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
using Paymongo.Sharp.Core.Entities;
using Paymongo.Sharp.Core.Enums;

#pragma warning disable CS8618

namespace Paymongo.Sharp.Payments.Entities
{
    public class Payment
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("payment_intent_id",
            NullValueHandling = NullValueHandling.Ignore)]
        public string PaymentIntentId { get; set; }
        
        [JsonProperty("balance_transaction_id",
            NullValueHandling = NullValueHandling.Ignore)]
        public string BalanceTransactionId { get; set; }
        
        [JsonProperty("description",
            NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
        
        [JsonProperty("amount",
            NullValueHandling = NullValueHandling.Ignore)]
        public int Amount { get; set; }
        
        [JsonProperty("fee",
            NullValueHandling = NullValueHandling.Ignore)]
        public int Fee { get; set; }
        
        [JsonProperty("net_amount",
            NullValueHandling = NullValueHandling.Ignore)]
        public int NetAmount { get; set; }
        
        [JsonProperty("currency",
            NullValueHandling = NullValueHandling.Ignore)]
        public Currency Currency { get; set; }
        
        [JsonProperty("billing",
            NullValueHandling = NullValueHandling.Ignore)]
        public Billing? Billing { get; set; }
        
        [JsonProperty("payout",
            NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string,string>? Payout { get; set; }
        
        [JsonProperty("source",
            NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string,string>? Source { get; set; }

        [JsonProperty("livemode")]
        public bool LiveMode { get; set; }
        
        [JsonProperty("status")]
        public PaymentStatus Status { get; set; }
        
        [JsonProperty("created_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? CreatedAt { get; set; }
        
        [JsonProperty("paid_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? PaidAt { get; set; }

        [JsonProperty("updated_at")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime? UpdatedAt { get; set; }
    }
}