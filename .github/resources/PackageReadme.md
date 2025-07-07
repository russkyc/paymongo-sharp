## Effortless PayMongo Integration with .NET

This library is an unofficial client that provides a simple and efficient way to integrate PayMongo's capabilities into
your .NET applications.

### Recommended Version for use

v1.0.0 (Latest)

### Client Support Status:

|    | API Resource      | Implementation Status  |
|----|-------------------|:----------------------:|
| ✅  | Checkout          |        **Full**        |
| ✅  | Links             |        **Full**        |
| ✅  | Payment Intent    |        **Full**        |
| ✅  | Payment Method    |        **Full**        |
| ✅  | Payments          |        **Full**        |
| ✅  | Card Installments |        **Full**        |
| ✅  | Refunds           |        **Full**        |
| ✅  | Sources           |        **Full**        |
| ✅  | QR PH             |        **Full**        |
| ✅  | Webhooks          |        **Full**        |
| ⚠️ | Customers         |      **Partial**       |
| ⛔  | Child Merchant    | _Unavailable, Planned_ |
| ⛔  | File Record       | _Unavailable, Planned_ |
| ⛔  | Platforms         | _Unavailable, Planned_ |
| ⛔  | Related Consumer  | _Unavailable, Planned_ |
| ⛔  | Requirements      | _Unavailable, Planned_ |
| ⛔  | Subscriptions     | _Unavailable, Planned_ |
| ⛔  | Treasury          | _Unavailable, Planned_ |

### Breaking Change (1.0.0):

All amount related properties should now be single number values, eg; 100.00 should be represented as 10000, to help
with this
there is a new extension method `ToLongAmount()` that can be used to convert decimal values to the correct amount
format.

```csharp

decimal amount = 100.00m;
// Convert to long amount (single number value in with centavos)
long longAmount = amount.ToLongAmount(); // 10000

```

This is to ensure that the library is consistent with the PayMongo API's expectations for amount values.

### Getting Started:

See info on how to get started in [repository](https://github.com/russkyc/paymongo-sharp)