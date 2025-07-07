﻿// MIT License
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

using Paymongo.Sharp.Tests.Utils;

namespace Paymongo.Sharp.Tests.Integration;

public class CustomerApiTests
{
    
    private readonly IPaymongoClient _client;

    public CustomerApiTests()
    {
        Env.TraversePath().Load();
        
        var secretKey = Env.GetString("SECRET_KEY");
        
        _client = new PaymongoClient(secretKey);
    }
    
    [Fact]
    async Task CreateAndDeleteCustomer()
    {
        // Arrange
        var customer = DataFakers.GenerateCustomer();
        
        // Act
        var customerResult = await _client.Customers.CreateCustomerAsync(customer);
        
        // Assert
        customerResult.Should().NotBeNull();
        customerResult.Id.Should().NotBeNullOrEmpty();
        customerResult.FirstName.Should().BeEquivalentTo(customer.FirstName);
        customerResult.LastName.Should().BeEquivalentTo(customer.LastName);
        customerResult.Email.Should().BeEquivalentTo(customer.Email);
        customerResult.Phone.Should().BeEquivalentTo(customer.Phone);
        customerResult.DefaultDevice.Should().Be(customer.DefaultDevice);

        bool deleteCustomerResult = await _client.Customers.DeleteCustomerAsync(customerResult.Id);

        deleteCustomerResult.Should().BeTrue();

    }

    [Fact]
    async Task CreateAndRetrieveCustomer()
    {
        // Arrange
        var customer = DataFakers.GenerateCustomer();
        
        // Act
        var customerResult = await _client.Customers.CreateCustomerAsync(customer);
        var getCustomersResult = await _client.Customers.RetrieveCustomerAsync(customer.Email, customer.Phone);
        
        // Assert
        getCustomersResult.Should().NotBeNull();
        getCustomersResult.Should().BeEquivalentTo(customerResult);
        
        // Cleanup
        var deleteCustomerResult = await _client.Customers.DeleteCustomerAsync(customerResult.Id);
        deleteCustomerResult.Should().BeTrue();
    }
    
    [Fact]
    async Task CreateAndEditCustomer()
    {
        // Arrange
        var customer = DataFakers.GenerateCustomer();
        
        // Act
        var customerResult = await _client.Customers.CreateCustomerAsync(customer);
        
        customerResult.FirstName = "New First Name";
        
        var editCustomerResult = await _client.Customers.EditCustomerAsync(customerResult);
        
        // Assert
        editCustomerResult.Should().NotBeNull();
        editCustomerResult.FirstName.Should().BeEquivalentTo(customerResult.FirstName);
        
        // Cleanup
        var deleteCustomerResult = await _client.Customers.DeleteCustomerAsync(customerResult.Id);
        deleteCustomerResult.Should().BeTrue();
    }
}