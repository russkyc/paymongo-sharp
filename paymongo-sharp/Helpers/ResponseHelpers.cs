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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Paymongo.Sharp.Checkouts.Entities;
using Paymongo.Sharp.Links.Entities;
using Paymongo.Sharp.Payments.Entities;
using Paymongo.Sharp.Sources.Entities;


#pragma warning disable CS8604
#pragma warning disable CS8601
#pragma warning disable CS8602

namespace Paymongo.Sharp.Helpers
{
    public static class ResponseHelpers
    {
        public static Checkout ToCheckout(this string? response)
        {
            dynamic checkoutRequestData = JObject.Parse(response);
            Checkout checkout = JsonConvert.DeserializeObject<Checkout>(checkoutRequestData.data!.attributes.ToString());
            
            // Checkout doesn't have an id field, so we take that from the parent
            checkout.Id = checkoutRequestData.data.id.ToString();
        
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            checkout.CreatedAt = checkout.CreatedAt.ToLocalDateTime();
            checkout.UpdatedAt = checkout.UpdatedAt.ToLocalDateTime();
            
            // Get payments
            string paymentsData = checkoutRequestData.data.attributes.payments.ToString();
            checkout.Payments = paymentsData.ToPayments();
            
            return checkout;
        }

        public static Payment ToPayment(this string? response)
        {
            dynamic paymentRequestData = JObject.Parse(response);

            Payment payment = JsonConvert.DeserializeObject<Payment>(paymentRequestData.data.attributes.ToString());
            
            // Payment doesn't have an id field so we get it from the parent
            payment.Id = paymentRequestData.data.id.ToString();
                    
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            payment.CreatedAt = payment.CreatedAt.ToLocalDateTime();
            payment.UpdatedAt = payment.UpdatedAt.ToLocalDateTime();
            payment.PaidAt = payment.PaidAt.ToLocalDateTime();
            
            return payment;
        }
        
        public static Source ToSource(this string? response)
        {
            dynamic sourceRequestData = JObject.Parse(response);

            Source source = JsonConvert.DeserializeObject<Source>(sourceRequestData.data.attributes.ToString());
            
            // Payment doesn't have an id field so we get it from the parent
            source.Id = sourceRequestData.data.id.ToString();
                    
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            source.CreatedAt = source.CreatedAt.ToLocalDateTime();
            source.UpdatedAt = source.UpdatedAt.ToLocalDateTime();
            
            return source;
        }
        
        public static Link ToLink(this string? response, bool isReferenceResource = false)
        {
            dynamic linkRequestData = JObject.Parse(response);

            dynamic linkData = isReferenceResource
                ? linkRequestData.data[0]
                : linkRequestData.data;

            Link link = JsonConvert.DeserializeObject<Link>(linkData.attributes.ToString());
            
            // Link doesn't have an id field so we get it from the parent
            link.Id = linkData.id;

            // Get payments
            string paymentsData = linkData.attributes.payments.ToString();
            link.Payments = paymentsData.ToDataPayments();
                    
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            link.CreatedAt = link.CreatedAt.ToLocalDateTime();
            link.UpdatedAt = link.UpdatedAt.ToLocalDateTime();
            
            return link;
        }
        
        public static IEnumerable<Payment> ToPayments(this string data)
        {
            var paymentRequestDataCollection = JsonConvert.DeserializeObject<IEnumerable<PaymentRequestAttributes>>(data);

            if (paymentRequestDataCollection is null)
            {
                return Enumerable.Empty<Payment>();
            }
            
            var paymentRequestArray = paymentRequestDataCollection.ToArray();

            if (!paymentRequestArray.Any())
            {
                return Enumerable.Empty<Payment>();
            }
            
            return paymentRequestArray.Select(paymentAttribute =>
            {
                var payment = paymentAttribute.Attributes;
                
                // Payment doesn't have an id field so we get it from the parent
                payment.Id = paymentAttribute.Id;
                    
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                payment.CreatedAt = payment.CreatedAt.ToLocalDateTime();
                payment.UpdatedAt = payment.UpdatedAt.ToLocalDateTime();
                payment.PaidAt = payment.PaidAt.ToLocalDateTime();
                
                return payment;
            });
        }
        
        public static IEnumerable<Payment> ToDataPayments(this string response)
        {
            var paymentRequestDataCollection = JsonConvert.DeserializeObject<IEnumerable<PaymentRequestData>>(response);

            if (paymentRequestDataCollection is null)
            {
                return Enumerable.Empty<Payment>();
            }
            
            return paymentRequestDataCollection.ToArray().Select(paymentAttribute =>
            {
                var payment = paymentAttribute.Data.Attributes;
                // Payment doesn't have an id field so we get it from the parent
                payment.Id = paymentAttribute.Data.Id;
                    
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                payment.CreatedAt = payment.CreatedAt.ToLocalDateTime();
                payment.UpdatedAt = payment.UpdatedAt.ToLocalDateTime();
                payment.PaidAt = payment.PaidAt.ToLocalDateTime();
                
                return payment;
            });
        }
        
    }
}