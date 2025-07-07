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

using Paymongo.Sharp.Features.Checkouts;
using Paymongo.Sharp.Features.Customers;
using Paymongo.Sharp.Features.Installments;
using Paymongo.Sharp.Features.Links;
using Paymongo.Sharp.Features.PaymentIntents;
using Paymongo.Sharp.Features.PaymentMethods;
using Paymongo.Sharp.Features.Payments;
using Paymongo.Sharp.Features.QrPh;
using Paymongo.Sharp.Features.Refunds;
using Paymongo.Sharp.Features.Sources;
using Paymongo.Sharp.Features.WebHooks;

namespace Paymongo.Sharp.Interfaces
{
    public interface IPaymongoClient
    {
        CheckoutClient Checkouts { get; }
        CardInstallmentsClient CardInstallments { get; set; }
        LinksClient Links { get; }
        PaymentClient Payments { get; }
        SourceClient Sources { get; }
        CustomerClient Customers { get; }
        PaymentIntentClient PaymentIntents { get; }
        PaymentMethodsClient PaymentMethods { get; }
        RefundClient Refunds { get; }
        QrPhClient QrPh { get; }
        WebhooksClient Webhooks { get; }
    }
}