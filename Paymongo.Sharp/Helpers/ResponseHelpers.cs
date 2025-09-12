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
using Paymongo.Sharp.Features.Checkouts.Contracts;
using Paymongo.Sharp.Features.Customers.Entities;
using Paymongo.Sharp.Features.Installments.Entities;
using Paymongo.Sharp.Features.Links.Contracts;
using Paymongo.Sharp.Features.PaymentIntents.Entities;
using Paymongo.Sharp.Features.PaymentMethods.Entities;
using Paymongo.Sharp.Features.Payments.Contracts;
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
            var schema = JsonSerializer.Deserialize<Checkout>(response);
            
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();
            
            return schema;
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
            var schema = JsonSerializer.Deserialize<Payment>(response);

            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();
            schema.Data.Attributes.PaidAt = schema.Data.Attributes.PaidAt.ToLocalDateTime();
            
            return schema;
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
                var referenceSchema = JsonSerializer.Deserialize<Schema<IList<Link>>>(response);
                var referenceLinkData = referenceSchema.Data[0];
                
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                referenceLinkData.Data.Attributes.CreatedAt = referenceLinkData.Data.Attributes.CreatedAt.ToLocalDateTime();
                referenceLinkData.Data.Attributes.UpdatedAt = referenceLinkData.Data.Attributes.UpdatedAt.ToLocalDateTime();

                return referenceLinkData;

            }

            var schema = JsonSerializer.Deserialize<Link>(response);
            var link = schema.Data.Attributes;
            
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = link.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = link.UpdatedAt.ToLocalDateTime();

            return schema;
        }
        
        internal static PaymentIntent ToPaymentIntent(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Schema<Data<PaymentIntent>>>(response);
            var paymentIntent = schema.Data.Attributes;
            
            // PaymentIntent doesn't have an id field so we get it from the parent
            paymentIntent.Id = schema.Data.Id;
                    
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            paymentIntent.CreatedAt = paymentIntent.CreatedAt.ToLocalDateTime();
            paymentIntent.UpdatedAt = paymentIntent.UpdatedAt.ToLocalDateTime();
            
            return paymentIntent;
        }

        internal static IEnumerable<InstallmentPlan> ToInstallmentPlans(this string? response)
        {
            if (response is null)
            {
                return Enumerable.Empty<InstallmentPlan>();
            }
            
            var schema = JsonSerializer.Deserialize<Schema<IList<InstallmentPlan>>>(response);
            var installmentPlans = schema.Data;

            return !installmentPlans.Any() ? Enumerable.Empty<InstallmentPlan>() : installmentPlans;
        }
        
        internal static IEnumerable<Payment> ToPayments(this string data)
        {
            var paymentRequestDataCollection = JsonSerializer.Deserialize<PaginatedSchema<PaymentData>>(data);

            if (paymentRequestDataCollection is null)
            {
                return Enumerable.Empty<Payment>();
            }
            
            var paymentRequestArray = paymentRequestDataCollection.Data.ToArray();

            if (!paymentRequestArray.Any())
            {
                return Enumerable.Empty<Payment>();
            }
            
            var paymentData = paymentRequestArray.Select(schema =>
            {
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                schema.Attributes.CreatedAt = schema.Attributes.CreatedAt.ToLocalDateTime();
                schema.Attributes.UpdatedAt = schema.Attributes.UpdatedAt.ToLocalDateTime();
                schema.Attributes.PaidAt = schema.Attributes.PaidAt.ToLocalDateTime();
                
                return schema;
            });

            return paymentData.Select(dataSchema => new Payment()
            {
                Data = dataSchema
            });
        }
        
        internal static IEnumerable<Customer> ToCustomers(this string? data)
        {
            var schema = JsonSerializer.Deserialize<Schema<IList<Data<Customer>>>>(data);
            var customers = schema.Data;

            if (!customers.Any())
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