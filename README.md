<img src=".github/resources/images/banner.svg" style="width: 100%;" />

<h2 align="center">Paymongo.Sharp - Unofficial .NET Client Wrapper for the Paymongo API</h2>

<p style="text-align: justify">
Paymongo is a powerful payment platform that provides a wide range of payment solutions for businesses of all sizes. With the Paymongo API, you can integrate payment processing into your .NET applications, allowing you to securely accept payments, manage transactions, and more.

This client wrapper is designed to make it easy for .NET developers to interact with the Paymongo API. It provides a simple, intuitive interface that abstracts away the complexity of the API, allowing you to focus on building your application rather than worrying about the details of payment processing.
</p>

> [!WARNING]  
> This is in it's early development stages and does not offer support for the whole
> Paymongo API. We recommend waiting for a stable release package before using this in your
> application. This is made available early only for testing purposes.

## What is currently Supported

This client is in active development, features are slowly being implemented but not all of them are supported as of now.
You can track the support for all of Paymongo's official API actions below:

### Checkout

- [ ] Create Checkout Session
- [ ] Retrieve a Checkout Session
- [ ] Expire a Checkout Session

### Payment Intent

- [ ] Create Payment Intent
- [ ] Retrieve a Payment Intent
- [ ] Attach to a Payment Intent

### Payment Method

- [ ] Retrieve list of possible merchant payment methods
- [ ] Create a payment method
- [ ] Retrieve a payment method
- [ ] Update a payment method

### Payments

- [ ] Create a Payment
- [ ] List all Payments
- [ ] Retrieve a Payment

### Links API

- [ ] Create a Link
- [ ] Retrieve a Link
- [ ] Get Link by Reference Number
- [ ] Archive a Link
- [ ] Unarchive a Link

### Webhooks

- [ ] Create a Webhook
- [ ] List all Webhooks
- [ ] Retrieve a Webhook
- [ ] Disable a Webhook
- [ ] Enable a Webhook
- [ ] Update a Webhook

### Refunds

- [ ] Create a Refund
- [ ] Retrieve a Refund
- [ ] List all Refunds

### Customers

- [ ] Create a Customer
- [ ] Retrieve a Customer
- [ ] Edit a Customer
- [ ] Delete a Customer
- [ ] Retrieve the Payment Methods of a Customer
- [ ] Delete a Payment Method of a Customer