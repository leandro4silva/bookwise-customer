﻿namespace BookWise.Customer.Infrastructure.Auths.Abstractions;

public interface ICognitoService
{
    Task<bool> RegisterCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken);

    Task<bool> UpdateImageCustomerAsync(string email, string picture, CancellationToken cancellationToken);

    Task<string> LoginCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken);
    
    Task<object> GetCustomerAsync(string email, CancellationToken cancellationToken);
}