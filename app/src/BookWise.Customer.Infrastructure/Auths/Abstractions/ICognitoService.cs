using Amazon.CognitoIdentityProvider.Model;

namespace BookWise.Customer.Infrastructure.Auths.Abstractions;

public interface ICognitoService
{
    Task<bool> RegisterCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken);

    Task<bool> UpdateImageCustomerAsync(string email, string picture, CancellationToken cancellationToken);

    Task<AuthenticationResultType> LoginCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken);

    Task<bool> ConfirmRegistrationAsync(string email, string confirmationCode, CancellationToken cancellationToken);
    
    Task<AdminGetUserResponse> GetCustomerAsync(string email, CancellationToken cancellationToken);
}