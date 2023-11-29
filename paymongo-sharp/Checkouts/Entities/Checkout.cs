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
using Newtonsoft.Json;
using paymongo_sharp.Core.Entities;
using paymongo_sharp.Core.Enums;

namespace paymongo_sharp.Checkouts.Entities
{
    public class Checkout
    {
        [JsonIgnore]
        public string? Id { get; set; }
        
        [JsonProperty("description")]
        public string? Description { get; set; }
        
        [JsonProperty("cancel_url")]
        public string? CancelUrl { get; set; }
        
        [JsonProperty("success_url")]
        public string? SuccessUrl { get; set; }
        
        [JsonProperty("checkout_url")]
        public string? CheckoutUrl { get; set; }
        
        [JsonProperty("billing")]
        public Billing? Billing { get; set; }
        
        [JsonProperty("payment_method_types")]
        public IEnumerable<PaymentMethod>? PaymentMethodTypes { get; set; }
        
        [JsonProperty("metadata")]
        public CheckoutMetadata? Metadata { get; set; }

        [JsonProperty("send_email_receipt")]
        public bool? SendEmailReceipt { get; set; }
        
        [JsonProperty("show_description")]
        public bool? ShowDescription { get; set; }
        
        [JsonProperty("show_line_items")]
        public bool? ShowLineItems { get; set; }

        [JsonProperty("status")]
        public CheckoutStatus? Status { get; set; }

        [JsonProperty("line_items")]
        public IEnumerable<LineItem>? LineItems { get; set; }
    }
}