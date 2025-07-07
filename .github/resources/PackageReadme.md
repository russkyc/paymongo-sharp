## Effortless PayMongo Integration with .NET
This library is an unofficial client that provides a simple and efficient way to integrate PayMongo's capabilities into your .NET applications.

### Recommended Version for use

v1.0.0 (Latest)

### Client Support Status:

<table>
  <thead>
    <tr>
      <th></th>
      <th>API Resource</th>
      <th style="text-align:center;">Implementation Status</th>
    </tr>
  </thead>
  <tbody>
    <!-- Full Support -->
    <tr>
      <td>✅</td>
      <td>Checkout</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Links</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Payment Intent</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Payment Method</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Payments</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Card Installments</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Refunds</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Sources</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>QR PH</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <tr>
      <td>✅</td>
      <td>Webhooks</td>
      <td style="text-align:center;"><b style="color:#e5f393;">Full</b></td>
    </tr>
    <!-- Partial Support -->
    <tr>
      <td>⚠️</td>
      <td>Customers</td>
      <td style="text-align:center;"><b style="color:#ffffff;">Partial</b></td>
    </tr>
    <!-- In Development / Unavailable -->
    <tr>
      <td>⛔</td>
      <td>Child Merchant</td>
      <td style="text-align:center;"><b style="color:gray;">Unavailable, Planned</b></td>
    </tr>
    <tr>
      <td>⛔</td>
      <td>File Record</td>
      <td style="text-align:center;"><b style="color:gray;">Unavailable, Planned</b></td>
    </tr>
    <tr>
      <td>⛔</td>
      <td>Platforms</td>
      <td style="text-align:center;"><b style="color:gray;">Unavailable, Planned</b></td>
    </tr>
    <tr>
      <td>⛔</td>
      <td>Related Consumer</td>
      <td style="text-align:center;"><b style="color:gray;">Unavailable, Planned</b></td>
    </tr>
    <tr>
      <td>⛔</td>
      <td>Requirements</td>
      <td style="text-align:center;"><b style="color:gray;">Unavailable, Planned</b></td>
    </tr>
    <tr>
      <td>⛔</td>
      <td>Subscriptions</td>
      <td style="text-align:center;"><b style="color:gray;">Unavailable, Planned</b></td>
    </tr>
    <tr>
      <td>⛔</td>
      <td>Treasury</td>
      <td style="text-align:center;"><b style="color:gray;">Unavailable, Planned</b></td>
    </tr>
  </tbody>
</table>

### Breaking Change (1.0.0):
All amount related properties should now be single number values, eg; 100.00 should be represented as 10000, to help with this
there is a new extension method `ToLongAmount()` that can be used to convert decimal values to the correct amount format.

```csharp

decimal amount = 100.00m;
// Convert to long amount (single number value in with centavos)
long longAmount = amount.ToLongAmount(); // 10000

```

This is to ensure that the library is consistent with the PayMongo API's expectations for amount values.

### Getting Started:

See info on how to get started in [repository](https://github.com/russkyc/paymongo-sharp)