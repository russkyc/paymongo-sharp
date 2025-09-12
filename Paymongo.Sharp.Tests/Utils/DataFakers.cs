// MIT License
// 
// Copyright (c) 2025 Russell Camo (@russkyc)
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

using Bogus;
using Paymongo.Sharp.Features.Customers.Contracts;
using Paymongo.Sharp.Features.PaymentMethods.Entities;
using Address = Paymongo.Sharp.Core.Entities.Address;

namespace Paymongo.Sharp.Tests.Utils;

internal static class DataFakers
{
    // Bogus/Faker currently does not support "en_PH" locale, so we use "en_US" for English.
    private const string Locale = "en_US";
    
    internal static CustomerAttributes GenerateCustomerAttributes()
    {
        return new Faker<CustomerAttributes>(Locale)
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber("+639#########")) // Philippine phone number format
            .RuleFor(c => c.DefaultDevice, f => f.PickRandom<Device>())
            .Generate();
    }
    
    internal static Details GenerateDetails()
    {
        return new Faker<Details>(Locale)
            .RuleFor(d => d.CardNumber, f => "4343434343434345") // Remove dashes
            .RuleFor(d => d.Cvc, f => f.Finance.CreditCardCvv())
            .RuleFor(d => d.ExpMonth, f => f.Random.Number(1, 12))
            .RuleFor(d => d.BankCode, f => f.PickRandom("test_bank_one", "test_bank_two")) // Placeholder for bank code
            .RuleFor(d => d.ExpYear, f => f.Random.Number(DateTime.Now.Year, 2050))
            .Generate();
    }

    internal static Billing GenerateBilling()
    {
        var addressFaker = new Faker<Address>(Locale)
            .RuleFor(a => a.Line1, f => f.Address.StreetAddress())
            .RuleFor(a => a.Line2, f => f.Address.SecondaryAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.State, f => f.Address.State())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Country, "PH"); // Philippines
        return new Faker<Billing>(Locale)
            .RuleFor(b => b.Name, f => f.Name.FullName())
            .RuleFor(b => b.Email, f => f.Internet.Email())
            .RuleFor(b => b.Phone, f => f.Phone.PhoneNumber("+639#########"))
            .RuleFor(b => b.Address, addressFaker)
            .Generate();
    }
}