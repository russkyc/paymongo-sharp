<img src=".github/resources/images/banner.svg" style="width: 100%;" />

<h2 align="center">Paymongo.Sharp - Unofficial .NET Client Wrapper for the Paymongo API</h2>

<p align="center">
    <a href="https://www.nuget.org/packages/Paymongo.Sharp">
        <img src="https://img.shields.io/nuget/v/Paymongo.Sharp?color=1f72de" alt="Nuget">
    </a>
    <a href="https://www.nuget.org/packages/Paymongo.Sharp">
        <img src="https://img.shields.io/badge/-.NET%20Standard%202.0-blueviolet?color=1f72de&label=NET" alt="">
    </a>
</p>

<p style="text-align: justify">
Paymongo is a powerful payment platform that provides a wide range of payment solutions for businesses of all sizes. With the Paymongo API, you can integrate payment processing into your .NET applications, allowing you to securely accept payments, manage transactions, and more.

This client wrapper is designed to make it easy for .NET developers to interact with the Paymongo API. It provides a simple, intuitive interface that abstracts the API, allowing you to access a typed client and focus on building your Paymongo-integrated application faster.
</p>

> [!WARNING]  
> This is in it's early development stages and does not offer support for the whole
> Paymongo API. We recommend waiting for a stable release package before using this in your
> application. This is made available early only for testing purposes.

#### Installation

Install pre-release package from: [Nuget](https://www.nuget.org/packages/Paymongo.Sharp/) using nuget cli or the nuget package manager.

#### Client usage

```csharp
var client = new PaymongoClient(publicKey: "<public_key>", secretKey: "<secret_key>");

// the client is now usable here
```

## :sparkles: What is currently Supported

This client is in active development, features are slowly being implemented but not all of them are supported as of now.
You can track the support for all of Paymongo's official API actions below:

---

### Checkout

- [x] Create Checkout Session
- [x] Retrieve a Checkout Session
- [x] Expire a Checkout Session

**Create a Checkout Session**

```csharp
// We creat a new Checkout object
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
Checkout checkoutResult = await _client.Checkouts.CreateCheckoutAsync(checkout);
```

**Retrieve a Checkout Session**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the checkout id is "12345678"
// This returns a Checkout object from the server
Checkout checkoutResult = await _client.Checkouts.RetrieveCheckoutAsync("12345678");
```

**Expire a Checkout Session**

```csharp
// We use the PaymongoClient from earlier
// Lets assume that the checkout id is "12345678"
// This expires the checkout on the server and returns the expired Checkout object
Checkout checkoutResult = await _client.Checkouts.ExpireCheckoutAsync("12345678");
```

---

### Payment Intent

- [ ] Create Payment Intent
- [ ] Retrieve a Payment Intent
- [ ] Attach to a Payment Intent

---

### Payment Method

- [ ] Retrieve list of possible merchant payment methods
- [ ] Create a payment method
- [ ] Retrieve a payment method
- [ ] Update a payment method

---

### Payments

- [ ] Create a Payment
- [ ] List all Payments
- [ ] Retrieve a Payment

---

### Links API

- [ ] Create a Link
- [ ] Retrieve a Link
- [ ] Get Link by Reference Number
- [ ] Archive a Link
- [ ] Unarchive a Link

---

### Webhooks

- [ ] Create a Webhook
- [ ] List all Webhooks
- [ ] Retrieve a Webhook
- [ ] Disable a Webhook
- [ ] Enable a Webhook
- [ ] Update a Webhook

---

### Refunds

- [ ] Create a Refund
- [ ] Retrieve a Refund
- [ ] List all Refunds

---

### Customers

- [ ] Create a Customer
- [ ] Retrieve a Customer
- [ ] Edit a Customer
- [ ] Delete a Customer
- [ ] Retrieve the Payment Methods of a Customer
- [ ] Delete a Payment Method of a Customer

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
