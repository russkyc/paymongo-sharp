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

using Paymongo.Sharp.Features.Customers.Contracts;
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
        var customer = new Customer()
        {
            Data = new CustomerData()
            {
                Attributes = DataFakers.GenerateCustomerAttributes()
            }
        };
        
        // Act
        var customerResult = await _client.Customers.CreateCustomerAsync(customer);
        
        // Assert
        customerResult.Should().NotBeNull();
        customerResult.Data.Id.Should().NotBeNullOrEmpty();
        customerResult.Data.Attributes.FirstName.Should().BeEquivalentTo(customer.Data.Attributes.FirstName);
        customerResult.Data.Attributes.LastName.Should().BeEquivalentTo(customer.Data.Attributes.LastName);
        customerResult.Data.Attributes.Email.Should().BeEquivalentTo(customer.Data.Attributes.Email);
        customerResult.Data.Attributes.Phone.Should().BeEquivalentTo(customer.Data.Attributes.Phone);
        customerResult.Data.Attributes.DefaultDevice.Should().Be(customer.Data.Attributes.DefaultDevice);

        bool deleteCustomerResult = await _client.Customers.DeleteCustomerAsync(customerResult.Data.Id);

        deleteCustomerResult.Should().BeTrue();

    }

    [Fact]
    async Task CreateAndRetrieveCustomer()
    {
        // Arrange
        var customer = new Customer()
        {
            Data = new CustomerData()
            {
                Attributes = DataFakers.GenerateCustomerAttributes()
            }
        };
        
        // Act
        var customerResult = await _client.Customers.CreateCustomerAsync(customer);
        var getCustomersResult = await _client.Customers.RetrieveCustomerAsync(customer.Data.Attributes.Email, customer.Data.Attributes.Phone);
        var getCustomersResultList = getCustomersResult.ToArray();
        var getCustomer = getCustomersResultList.First();
        // Assert
        getCustomersResultList.Should().NotBeNull();
        getCustomersResultList.First().Should().BeEquivalentTo(customerResult.Data);
        
        // Cleanup
        var deleteCustomerResult = await _client.Customers.DeleteCustomerAsync(getCustomer.Id);
        deleteCustomerResult.Should().BeTrue();
    }
    
    [Fact]
    async Task CreateAndEditCustomer()
    {
        // Arrange
        var customer = new Customer()
        {
            Data = new CustomerData()
            {
                Attributes = DataFakers.GenerateCustomerAttributes()
            }
        };
        
        // Act
        var customerResult = await _client.Customers.CreateCustomerAsync(customer);
        
        customerResult.Data.Attributes.FirstName = "New First Name";
        
        var editCustomerResult = await _client.Customers.EditCustomerAsync(customerResult);
        
        // Assert
        editCustomerResult.Should().NotBeNull();
        editCustomerResult.Data.Attributes.FirstName.Should().BeEquivalentTo(customerResult.Data.Attributes.FirstName);
        
        // Cleanup
        var deleteCustomerResult = await _client.Customers.DeleteCustomerAsync(customerResult.Data.Id);
        deleteCustomerResult.Should().BeTrue();
    }
}