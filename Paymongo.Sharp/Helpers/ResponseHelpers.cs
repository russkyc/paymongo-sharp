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
using System.Linq;
using System.Text.Json;
using Paymongo.Sharp.Core.Contracts;
using Paymongo.Sharp.Features.Checkouts.Entities;
using Paymongo.Sharp.Features.Customers.Entities;
using Paymongo.Sharp.Features.Links.Entities;
using Paymongo.Sharp.Features.PaymentMethods.Entities;
using Paymongo.Sharp.Features.Payments.Entities;
using Paymongo.Sharp.Features.Refunds.Entities;
using Paymongo.Sharp.Features.Sources.Entities;
using Paymongo.Sharp.Features.WebHooks.Entities;


#pragma warning disable CS8604
#pragma warning disable CS8601
#pragma warning disable CS8602

namespace Paymongo.Sharp.Helpers
{
    internal static class ResponseHelpers
    {
        internal static Boolean ToCustomerResultBool(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<Customer>>>(response);

            var result = schema.Data.Attributes.Deleted ?? false;
            
            return result;
        }
        
        internal static Checkout ToCheckout(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<Checkout>>>(response);
            var checkout = schema.Data.Attributes;
            
            // Checkout doesn't have an id field, so we take that from the parent
            checkout.Id = schema.Data.Id;
        
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            checkout.CreatedAt = checkout.CreatedAt.ToLocalDateTime();
            checkout.UpdatedAt = checkout.UpdatedAt.ToLocalDateTime();
            
            return checkout;
        }

        internal static Refund ToRefund(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<Refund>>>(response);
            var refund = schema.Data.Attributes;

            // Refund doesn't have an id field, so we take that from the parent
            refund.Id = schema.Data.Id;

            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            refund.CreatedAt = refund.CreatedAt.ToLocalDateTime();
            refund.UpdatedAt = refund.UpdatedAt.ToLocalDateTime();

            return refund;
        }

        internal static IEnumerable<Refund> ToRefunds(this string data)
        {
            var refundRequestDataCollection = JsonSerializer.Deserialize<IEnumerable<Data<Refund>>>(data);

            if (refundRequestDataCollection is null)
            {
                return Enumerable.Empty<Refund>();
            }

            var refundRequestArray = refundRequestDataCollection.ToArray();

            if (!refundRequestArray.Any())
            {
                return Enumerable.Empty<Refund>();
            }

            return refundRequestArray.Select(refundAttribute =>
            {
                var refund = refundAttribute.Attributes;

                // refund doesn't have an id field so we get it from the parent
                refund.Id = refundAttribute.Id;

                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                refund.CreatedAt = refund.CreatedAt.ToLocalDateTime();
                refund.UpdatedAt = refund.UpdatedAt.ToLocalDateTime();

                return refund;
            });
        }

        internal static Customer ToCustomer(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<Customer>>>(response);
            var customer = schema.Data.Attributes;
            
            // Checkout doesn't have an id field, so we take that from the parent
            customer.Id = schema.Data.Id;
        
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            customer.CreatedAt = customer.CreatedAt.ToLocalDateTime();
            customer.UpdatedAt = customer.UpdatedAt.ToLocalDateTime();
            
            return customer;
        }

        internal static Payment ToPayment(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<Payment>>>(response);

            var payment = schema.Data.Attributes;
            
            // Payment doesn't have an id field so we get it from the parent
            payment.Id = schema.Data.Id;
                    
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            payment.CreatedAt = payment.CreatedAt.ToLocalDateTime();
            payment.UpdatedAt = payment.UpdatedAt.ToLocalDateTime();
            payment.PaidAt = payment.PaidAt.ToLocalDateTime();
            
            return payment;
        }
        
        internal static PaymentMethod ToPaymentMethod(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<PaymentMethod>>>(response);

            var paymentMethod = schema.Data.Attributes;
            
            // Payment doesn't have an id field so we get it from the parent
            paymentMethod.Id = schema.Data.Id;
                    
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            paymentMethod.CreatedAt = paymentMethod.CreatedAt.ToLocalDateTime();
            paymentMethod.UpdatedAt = paymentMethod.UpdatedAt.ToLocalDateTime();
            
            return paymentMethod;
        }
        
        internal static IEnumerable<PaymentMethod> ToPaymentMethods(this string? data)
        {
            if (data is null)
            {
                return Enumerable.Empty<PaymentMethod>();
            }
            
            var paymentRequestDataCollection = JsonSerializer.Deserialize<IEnumerable<Data<PaymentMethod>>>(data);

            if (paymentRequestDataCollection is null)
            {
                return Enumerable.Empty<PaymentMethod>();
            }
            
            var paymentRequestArray = paymentRequestDataCollection.ToArray();

            if (!paymentRequestArray.Any())
            {
                return Enumerable.Empty<PaymentMethod>();
            }
            
            return paymentRequestArray.Select(paymentAttribute =>
            {
                var payment = paymentAttribute.Attributes;
                
                // Payment doesn't have an id field so we get it from the parent
                payment.Id = paymentAttribute.Id;
                    
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                payment.CreatedAt = payment.CreatedAt.ToLocalDateTime();
                payment.UpdatedAt = payment.UpdatedAt.ToLocalDateTime();
                
                return payment;
            });
        }
        
        internal static Source ToSource(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<Source>>>(response);
            
            var source = schema.Data.Attributes;
            
            // Payment doesn't have an id field so we get it from the parent
            source.Id = schema.Data.Id;
                    
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            source.CreatedAt = source.CreatedAt.ToLocalDateTime();
            source.UpdatedAt = source.UpdatedAt.ToLocalDateTime();
            
            return source;
        }
        
        internal static Webhook ToWebHook(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<Webhook>>>(response);
            var webHook = schema.Data.Attributes;
            
            // Webhook doesn't have an id field so we get it from the parent
            webHook.Id = schema.Data.Id;
            
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            webHook.CreatedAt = webHook.CreatedAt.ToLocalDateTime();
            webHook.UpdatedAt = webHook.UpdatedAt.ToLocalDateTime();
            
            return webHook;
        }
        
        internal static IEnumerable<Webhook> ToWebHookList(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<IList<Data<Webhook>>>>(response);
            var webHooks = schema.Data;

            if (!webHooks.Any())
            {
                return Enumerable.Empty<Webhook>();
            }
            
            return webHooks.Select(webHookData =>
            {
                var webHook = webHookData.Attributes;
                
                // Webhook doesn't have an id field so we get it from the parent
                webHook.Id = webHookData.Id;
                
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                webHook.CreatedAt = webHook.CreatedAt.ToLocalDateTime();
                webHook.UpdatedAt = webHook.UpdatedAt.ToLocalDateTime();
                
                return webHook;
            });
        }
        
        internal static Link ToLink(this string? response, bool isReferenceResource = false)
        {
            if (isReferenceResource)
            {
                var referenceSchema = JsonSerializer.Deserialize<Schema<IList<Data<Link>>>>(response);
                var referenceLinkData = referenceSchema.Data[0];
                var referenceLink = referenceLinkData.Attributes;
                
                // Link doesn't have an id field so we get it from the parent
                referenceLink.Id = referenceLinkData.Id;
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                referenceLink.CreatedAt = referenceLink.CreatedAt.ToLocalDateTime();
                referenceLink.UpdatedAt = referenceLink.UpdatedAt.ToLocalDateTime();

                return referenceLink;

            }

            var schema = JsonSerializer.Deserialize<Schema<Data<Link>>>(response);
            var linkData = schema.Data;
            var link = linkData.Attributes;
            
            // Link doesn't have an id field so we get it from the parent
            link.Id = schema.Data.Id;
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            link.CreatedAt = link.CreatedAt.ToLocalDateTime();
            link.UpdatedAt = link.UpdatedAt.ToLocalDateTime();

            return link;
        }
        
        internal static IEnumerable<Payment> ToPayments(this string data)
        {
            var paymentRequestDataCollection = JsonSerializer.Deserialize<Schema<IEnumerable<Data<Payment>>>>(data);

            if (paymentRequestDataCollection is null)
            {
                return Enumerable.Empty<Payment>();
            }
            
            var paymentRequestArray = paymentRequestDataCollection.Data.ToArray();

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
        
        internal static IEnumerable<Payment> ToDataPayments(this string response)
        {
            var paymentRequestDataCollection = JsonSerializer.Deserialize<IEnumerable<Schema<Data<Payment>>>>(response);

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
        internal static IEnumerable<Customer> ToCustomers(this string? data)
        {
            var schema = JsonSerializer.Deserialize<Schema<IList<Data<Customer>>>>(data);
            var customers = schema.Data;

            if (customers is null || !customers.Any())
            {
                return Enumerable.Empty<Customer>();
            }
            
            return customers.Select(requestData =>
            {
                var customer = requestData.Attributes;
                
                // Customer doesn't have an id field so we get it from the parent
                customer.Id = requestData.Id;
                
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                customer.CreatedAt = customer.CreatedAt.ToLocalDateTime();
                customer.UpdatedAt = customer.UpdatedAt.ToLocalDateTime();
                
                return customer;
            });
        }
    }
}