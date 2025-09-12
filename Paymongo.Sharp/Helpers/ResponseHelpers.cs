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
using Paymongo.Sharp.Features.CardInstallments.Contracts;
using Paymongo.Sharp.Features.Checkouts.Contracts;
using Paymongo.Sharp.Features.Customers.Contracts;
using Paymongo.Sharp.Features.Links.Contracts;
using Paymongo.Sharp.Features.PaymentIntents.Contracts;
using Paymongo.Sharp.Features.PaymentMethods.Entities;
using Paymongo.Sharp.Features.Payments.Contracts;
using Paymongo.Sharp.Features.Refunds.Contracts;
using Paymongo.Sharp.Features.Sources.Entities;
using Paymongo.Sharp.Features.WebHooks.Contracts;


#pragma warning disable CS8604
#pragma warning disable CS8601
#pragma warning disable CS8602

namespace Paymongo.Sharp.Helpers
{
    internal static class ResponseHelpers
    {
        internal static Boolean ToCustomerResultBool(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Customer>(response);
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
            var schema = JsonSerializer.Deserialize<Refund>(response);
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();

            return schema;
        }

        internal static IEnumerable<RefundData> ToRefunds(this string data)
        {
            var refundRequestDataCollection = JsonSerializer.Deserialize<PaginatedSchema<RefundData>>(data);

            if (refundRequestDataCollection is null)
            {
                return Enumerable.Empty<RefundData>();
            }

            var refundRequestArray = refundRequestDataCollection.Data.ToArray();

            if (!refundRequestArray.Any())
            {
                return Enumerable.Empty<RefundData>();
            }

            return refundRequestArray.Select(refundData =>
            {
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                refundData.Attributes.CreatedAt = refundData.Attributes.CreatedAt.ToLocalDateTime();
                refundData.Attributes.UpdatedAt = refundData.Attributes.UpdatedAt.ToLocalDateTime();

                return refundData;
            });
        }

        internal static Customer ToCustomer(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Customer>(response);
            
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();
            
            return schema;
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
            var schema = JsonSerializer.Deserialize<PaymentMethod>(response);

            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();
            
            return schema;
        }
        
        internal static IEnumerable<PaymentMethod> ToPaymentMethods(this string? data)
        {
            if (data is null)
            {
                return Enumerable.Empty<PaymentMethod>();
            }
            
            var paymentRequestDataCollection = JsonSerializer.Deserialize<IEnumerable<PaymentMethod>>(data);

            if (paymentRequestDataCollection is null)
            {
                return Enumerable.Empty<PaymentMethod>();
            }
            
            var paymentRequestArray = paymentRequestDataCollection.ToArray();

            if (!paymentRequestArray.Any())
            {
                return Enumerable.Empty<PaymentMethod>();
            }
            
            return paymentRequestArray.Select(paymentData =>
            {
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                paymentData.Data.Attributes.CreatedAt = paymentData.Data.Attributes.CreatedAt.ToLocalDateTime();
                paymentData.Data.Attributes.UpdatedAt = paymentData.Data.Attributes.UpdatedAt.ToLocalDateTime();
                
                return paymentData;
            });
        }
        
        internal static Source ToSource(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Source>(response);
            
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();
            
            return schema;
        }
        
        internal static Webhook ToWebHook(this string? response)
        {
            var schema = JsonSerializer.Deserialize<Webhook>(response);
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();
            
            return schema;
        }
        
        internal static IEnumerable<WebhookData> ToWebHookList(this string? response)
        {
            var schema = JsonSerializer.Deserialize<PaginatedSchema<WebhookData>>(response);
            var webHooks = schema.Data.ToArray();

            if (!webHooks.Any())
            {
                return Enumerable.Empty<WebhookData>();
            }
            
            return webHooks.Select(webHookData =>
            {
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                webHookData.Attributes.CreatedAt = webHookData.Attributes.CreatedAt.ToLocalDateTime();
                webHookData.Attributes.UpdatedAt = webHookData.Attributes.UpdatedAt.ToLocalDateTime();
                
                return webHookData;
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
            var schema = JsonSerializer.Deserialize<PaymentIntent>(response);
            
            // Unix timestamp doesn't account for daylight savings, so we adjust it here
            schema.Data.Attributes.CreatedAt = schema.Data.Attributes.CreatedAt.ToLocalDateTime();
            schema.Data.Attributes.UpdatedAt = schema.Data.Attributes.UpdatedAt.ToLocalDateTime();
            
            return schema;
        }

        internal static IEnumerable<Plan> ToInstallmentPlans(this string? response)
        {
            if (response is null)
            {
                return Enumerable.Empty<Plan>();
            }
            
            var schema = JsonSerializer.Deserialize<PaginatedSchema<Plan>>(response);
            var installmentPlans = schema.Data.ToArray();

            return !installmentPlans.Any() ? Enumerable.Empty<Plan>() : installmentPlans;
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
        
        internal static IEnumerable<CustomerData> ToCustomers(this string? data)
        {
            var schema = JsonSerializer.Deserialize<PaginatedSchema<CustomerData>>(data);
            var customers = schema.Data.ToArray();

            if (!customers.Any())
            {
                return Enumerable.Empty<CustomerData>();
            }
            
            return customers.Select(customerData =>
            {
                // Unix timestamp doesn't account for daylight savings, so we adjust it here
                customerData.Attributes.CreatedAt = customerData.Attributes.CreatedAt.ToLocalDateTime();
                customerData.Attributes.UpdatedAt = customerData.Attributes.UpdatedAt.ToLocalDateTime();
                
                return customerData;
            });
        }
    }
}