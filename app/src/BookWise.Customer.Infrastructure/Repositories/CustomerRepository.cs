﻿using Amazon.DynamoDBv2.DataModel;
using BookWise.Customer.Domain.Repositories;

namespace BookWise.Customer.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IDynamoDBContext _dynamoDBContext;

    public CustomerRepository(IDynamoDBContext dynamoDBContext)
    {
        _dynamoDBContext = dynamoDBContext;
    }

    public async Task AddAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken)
    {
        await _dynamoDBContext.SaveAsync<Domain.Entities.Customer>(customer, cancellationToken);
    }

    public Task<Domain.Entities.Customer> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}