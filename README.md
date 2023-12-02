<a href="https://www.nuget.org/packages/Paymongo.Sharp">
    <img src=".github/resources/images/banner.svg" style="width: 100%;" />
</a>

<h2 align="center">Paymongo.Sharp - Unofficial .NET Client Wrapper for the Paymongo API</h2>

<p align="center">
    <img src="https://img.shields.io/nuget/v/Paymongo.Sharp?color=1f72de" alt="Nuget">
    <img src="https://img.shields.io/badge/-.NET%20Standard%202.0-blueviolet?color=1f72de&label=NET" alt="">
    <img src="https://img.shields.io/github/license/russkyc/paymongo-sharp">
    <img src="https://img.shields.io/github/issues/russkyc/paymongo-sharp">
</p>

<p style="text-align: justify">
Paymongo is a powerful payment platform that provides a wide range of payment solutions for businesses of all sizes. With the Paymongo API, you can integrate payment processing into your .NET applications, allowing you to securely accept payments, manage transactions, and more.

This client wrapper is designed to make it easy for .NET developers to interact with the Paymongo API. It provides a simple, intuitive interface that abstracts the API, allowing you to access a typed client and focus on building your Paymongo-integrated application faster.
</p>

> [!NOTE]  
> This is in it's early development stages and does not offer support for the whole
> Paymongo API. But if you only need checkouts and links you can already use this as
> both are already fully implemented.

## :sparkles: What is currently Supported

This client is in active development and features are slowly being implemented but not all of them are supported as of now.
You can track the support for all of Paymongo's official API actions below:

### API Support (Paymongo API v1)
<table>
  <tr>
    <th>API Resource</th>
    <th>Status</th>
    <th>Added on</th>
  </tr>
  <tr>
    <td>Checkout</td>
    <td>Full</td>
    <td>v0.1.2-pre+</td>
  </tr>
  <tr>
    <td>Payment Intent</td>
    <td>Not yet available</td>
    <td>Not yet Added</td>
  </tr>
  <tr>
    <td>Payment Method</td>
    <td>Not yet available</td>
    <td>Not yet Added</td>
  </tr>
  <tr>
    <td>Payments</td>
    <td>Partial</td>
    <td>v0.2.0+</td>
  </tr>
  <tr>
    <td>Links</td>
    <td>Full</td>
    <td>v0.3.0+</td>
  </tr>
  <tr>
    <td>Webhooks</td>
    <td>Not yet available</td>
    <td>Not yet Added</td>
  </tr>
  <tr>
    <td>Customers</td>
    <td>Not yet available</td>
    <td>Not yet Added</td>
  </tr>
  <tr>
    <td>Treasury</td>
    <td>Not yet available</td>
    <td>Not yet Added</td>
  </tr>
  <tr>
    <td>Installments</td>
    <td>Not yet available</td>
    <td>Not yet Added</td>
  </tr>
  <tr>
    <td>Sources</td>
    <td>Not yet available</td>
    <td>Not yet Added</td>
  </tr>
</table>

---

## :arrow_down: Installation

Install package from: [Nuget](https://www.nuget.org/packages/Paymongo.Sharp/) using nuget cli or the nuget package manager.

#### Client usage

Add Imports

```csharp
using Paymongo.Sharp;
```

Initialize Client
```csharp
var client = new PaymongoClient(secretKey: "<secret_key>");
```

---

## Basic Client API Reference

If you want to get started with this Paymongo client, you can
check the refernces for basic usage below.

### Checkout

- [x] Create Checkout Session
- [x] Retrieve a Checkout Session
- [x] Expire a Checkout Session

**Create a Checkout Session**

```csharp
// We create a new Checkout object
// This one includes the minimal required values
Checkout checkout = new Checkout() {
  Description = "Test Checkout",
    LineItems = new [] {
      new LineItem {
        Name = "item_name",
          Quantity = 1,
          Currency = Currency.Php,
          Amount = 3500
      }
    },
    PaymentMethodTypes = new [] {
      PaymentMethod.GCash,
        PaymentMethod.Card,
        PaymentMethod.Paymaya
    }
};

// We use the PaymongoClient from earlier
// This returns the Checkout object with the new server info for checkout url, id, and others
Checkout checkoutResult = await client.Checkouts.CreateCheckoutAsync(checkout);
```

**Retrieve a Checkout Session**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the checkout id is "12345678"
// This returns a Checkout object from the server
Checkout checkoutResult = await client.Checkouts.RetrieveCheckoutAsync("12345678");
```

**Expire a Checkout Session**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the checkout id is "12345678"
// This expires the checkout on the server and returns the expired Checkout object
Checkout checkoutResult = await client.Checkouts.ExpireCheckoutAsync("12345678");
```

For full Checkout API reference, please see: [Checkout Session Resource](https://developers.paymongo.com/reference/checkout-session-resource)

---

### Payment Intent

- [ ] Create Payment Intent
- [ ] Retrieve a Payment Intent
- [ ] Attach to a Payment Intent
- [ ] Attach to a Payment Intent

Pre Authorization and Capture

- [ ] Capture a Payment Intent
- [ ] Cancel a Payment Intent

For full Payment API reference, please see: [The Payment Intent Object](https://developers.paymongo.com/reference/the-payment-intent-object), [(Pre-Authorization) Capture](https://developers.paymongo.com/reference/capture-a-payment), [(Pre-Authorization) Cancel](https://developers.paymongo.com/reference/cancel-a-payment)

---

### Payment Method

- [ ] Retrieve list of possible merchant payment methods
- [ ] Create a payment method
- [ ] Retrieve a payment method
- [ ] Update a payment method

For full Payment Method API reference, please see: [The Payment Method Object](https://developers.paymongo.com/reference/the-payment-method-object)

---

### Payments

- [ ] Create a Payment
- [x] List all Payments
- [x] Retrieve a Payment

**List All Payments**

```csharp
// We use the PaymongoClient from earlier
// This returns a list of Payment objects
IEnumerable<Payment> paymentsResult = await client.Payments.ListAllPaymentsAsync();
```

**Retrieve a Payment**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the payment id is "12345678"
// This returns a Payment object from the server
Payment paymentResult = await client.Payments.RetrievePaymentAsync("12345678");
```

For full Payments API reference, please see: [Payment Resource](https://developers.paymongo.com/reference/payment-source)

---

### Links API

- [x] Create a Link
- [x] Retrieve a Link
- [x] Get Link by Reference Number
- [x] Archive a Link
- [x] Unarchive a Link

**Create a Link**

```csharp
// We create a new Link object
// This one includes the minimal required values
Link link = new Link {
  Description = "New Link",
  Amount = 100000,
  Currency = Currency.Php
};

// We use the PaymongoClient from earlier
// This returns the Link object with the new server info for checkout url, id, and others
var linkResult = await client.Links.CreateLinkAsync(link);
```

**Retrieve a Link**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the link id is "12345678"
// This returns a Link object from the server
Link linkResult = await client.Links.RetrieveLinkAsync("12345678");
```

**Get Link by Reference Number**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the link id is "ABCD1234"
// This returns a Link object from the server
Link linkResult = await client.Links.GetLinkByReferenceNumberAsync("ABCD1234");
```

**Archive a Link**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the Link id is "12345678"
// This returns the updated archived Link object
Link linkResult = await client.Links.ArchiveLinkAsync("12345678");
```

**Unarchive a Link**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the payment id is "12345678"
// This returns the updated un-archived Link object
Link linkResult = await client.Links.UnarchiveLinkAsync("12345678");
```

For full Links API reference, please see: [Links Resource](https://developers.paymongo.com/reference/links-resource)

---

### Webhooks

- [ ] Create a Webhook
- [ ] List all Webhooks
- [ ] Retrieve a Webhook
- [ ] Disable a Webhook
- [ ] Enable a Webhook
- [ ] Update a Webhook

For full Webhook API reference, please see: [Webhook Resource](https://developers.paymongo.com/reference/webhook-resource)

---

### Refunds

- [ ] Create a Refund
- [ ] Retrieve a Refund
- [ ] List all Refunds

For full Refunds API reference, please see: [Refund Resource](https://developers.paymongo.com/reference/refund-resource)

---

### Customers

- [ ] Create a Customer
- [ ] Retrieve a Customer
- [ ] Edit a Customer
- [ ] Delete a Customer
- [ ] Retrieve the Payment Methods of a Customer
- [ ] Delete a Payment Method of a Customer

For full Customers API reference, please see: [Customer Resource](https://developers.paymongo.com/reference/customer-resource)

---

### Sources (Gcash and GrabPay Checkout)

- [ ] Create a Source
- [ ] Retrieve a Source

For full Sources API reference, please see: [The Sources Object](https://developers.paymongo.com/reference/the-sources-object)

---

### Treasury

Treasury has a couple of sub sections

##### Wallet

- [ ] Retrieve Wallet by ID
- [ ] Retrieve Wallet Accounts

For full Transaction History API reference, please see: [Wallet Account Resource](https://developers.paymongo.com/reference/account-resource)

##### Send Money

- [ ] Create a Wallet Transaction
- [ ] Retrieve List of all Receiving Institutions

For full Send Money API reference, please see: [Wallet Transaction Resource](https://developers.paymongo.com/reference/wallet-transaction-resource)

##### Disbursemenet

- [ ] Create a Batch Transaction

For full Transaction History API reference, please see: [Batch Transaction Resource](https://developers.paymongo.com/reference/batch-transaction-resource)

##### Transaction History

- [ ] Retrieve a Wallet Transaction By ID
- [ ] Retrieve List of Wallet Transactions
- [ ] Retrieve List of Batches
- [ ] Retrieve Batch Object

For full Transaction History API reference, please see: [Retrieve a Wallet Transaction By ID](https://developers.paymongo.com/reference/retrieve-a-wallet-transaction)

---

### Installments

- [ ] List Installment Plans

For full Installments API reference, please see: [List Installment Plans](https://developers.paymongo.com/reference/list-installment-plans)

---

## Future Plans

### Fluent Builder
Something that I have in mind that might be easier to work with,
for context let's compare the current api usage and the future
fluent builder implementation for the Checkouts client.

##### 1. Current

```csharp
var client = new PaymongoClient(secretKey: "<secret_key>");

Checkout checkout = new Checkout() {
  Description = "Test Checkout",
    LineItems = new [] {
      new LineItem {
        Name = "Item Name",
          Quantity = 1,
          Currency = Currency.Php,
          Amount = 3500
      }
    },
    PaymentMethodTypes = new [] {
      PaymentMethod.GCash,
        PaymentMethod.Card,
        PaymentMethod.Paymaya
    }
};

Checkout checkoutResult = await client.Checkouts.CreateCheckoutAsync(checkout);
```

##### 2. Future Fluent Builder

```csharp
var client = new PaymongoClient(secretKey: "<secret_key>");

Checkout checkoutResult = await CheckoutBuilder
                                    .WithDescription("Test Checkout")
                                    .WithLineItem(
                                        LineItemBuilder.WithName("Item Name")
                                            .WithQuantity(1)
                                            .WithAmount(3500)
                                            .WithCurrency(Currency.Php)
                                    )
                                    .WithPaymentMethod(PaymentMethod.Gcash)
                                    .WithPaymentMethod(PaymentMethod.Card)
                                    .WithPaymentMethod(PaymentMethod.Paymaya)
                                    .CreateCheckoutAsync();
```

### Other Plans

What do you think should be implemented in future versions of the client? Let your ideas be known
and open an [issue](https://github.com/russkyc/paymongo-sharp/issues) with a [feature-request] tag and it might make it into future updates.
Or, if you tried something that works and is awesome try opening a [pull request](https://github.com/russkyc/paymongo-sharp/pulls) and if all
is good, your contribution can be implemented into the project!

---

## :heart: Donate

This is free and available for everyone to use, but still requires time for development
and maintenance. By choosing to donate, you are not only helping develop this project,
but you are also helping me dedicate more time for creating more tools that help the community :heart:

## :tada: Special Thanks

This project is made easier to develop by Jetbrains! They have provided
Licenses to their IDE's to support development of this open-source project.

<a href="https://www.jetbrains.com/community/opensource/#support">
<img width="200px" src="https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.png" alt="JetBrains Logo (Main) logo.">
</a>
