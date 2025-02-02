using System.Net;
using System.Security.Cryptography;
using System.Text;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using BookWise.Customer.Infrastructure.Auths.Abstractions;
using BookWise.Customer.Infrastructure.Auths.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using InternalErrorException = Amazon.CognitoIdentity.Model.InternalErrorException;

namespace BookWise.Customer.Infrastructure.Auths.Services;

public sealed class CognitoService : ICognitoService
{
    private readonly CognitoConfig _cognitoConfig;
    private readonly IAmazonCognitoIdentityProvider _cognitoProvider;
    private readonly ILogger<CognitoService> _logger;

    public CognitoService(IOptionsMonitor<CognitoConfig> cognitoConfig, IAmazonCognitoIdentityProvider cognitoProvider,
        ILogger<CognitoService> logger)
    {
        _cognitoProvider = cognitoProvider;
        _cognitoConfig = cognitoConfig.CurrentValue;
        _logger = logger;
    }
    
    public async Task<bool> RegisterCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken)
    {
        var request = new SignUpRequest
        {
            ClientId = _cognitoConfig.ClientId,
            SecretHash = GenerateSecretHash(customer.Email!, _cognitoConfig.ClientId!, _cognitoConfig.ClientSecret!),
            Username = customer.Email,
            Password = customer.Password,
            UserAttributes = new List<AttributeType>
            {
                new() { Name = "name", Value = customer.FullName },
                new() { Name = "birthdate", Value = customer.BirthDate.ToString("yyyy-MM-dd")! },
                new() { Name = "picture", Value = customer.Image },
                new() { Name = "address", Value = customer.Address!.ToString() },
                new() { Name = "phone_number", Value = customer.PhoneNumber },
            }
        };
        
        try
        {
            var response = await _cognitoProvider.SignUpAsync(request, cancellationToken);
            return response.HttpStatusCode == HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<AuthenticationResultType> LoginCustomerAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken)
    {
        var secretHash = GenerateSecretHash(customer.Email!, _cognitoConfig.ClientId!, _cognitoConfig.ClientSecret!);

        var authRequest = new AdminInitiateAuthRequest
        {
            AuthFlow = AuthFlowType.ADMIN_USER_PASSWORD_AUTH,
            UserPoolId = _cognitoConfig.PoolId,
            ClientId = _cognitoConfig.ClientId,
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", customer.Email! },
                { "PASSWORD", customer.Password! },
                { "SECRET_HASH", secretHash }
            }
        };

        try
        {
            var authResponse = await _cognitoProvider.AdminInitiateAuthAsync(authRequest, cancellationToken);
            return authResponse.AuthenticationResult;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred during authentication: {Message}", ex.Message);
            throw;
        }
    }

    public async Task<AdminGetUserResponse> GetCustomerAsync(string email, CancellationToken cancellationToken)
    {
        try
        {
            var request = new AdminGetUserRequest
            {
                UserPoolId = _cognitoConfig.PoolId,
                Username = email
            };

            var response = await _cognitoProvider.AdminGetUserAsync(request, cancellationToken);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<bool> UpdateImageCustomerAsync(string email, string picture, CancellationToken cancellationToken)
    {
        var updateRequest = new AdminUpdateUserAttributesRequest
        {
            UserPoolId = _cognitoConfig.PoolId,
            Username = email,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType
                {
                    Name = "picture", Value = picture
                }
            }
        };

        try
        {
            var response = await _cognitoProvider.AdminUpdateUserAttributesAsync(updateRequest, cancellationToken);
            return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
    
    /// <summary>
    /// Gera o SecretHash necessário para autenticação no Cognito quando o App Client possui Secret ativado.
    /// </summary>
    private string GenerateSecretHash(string username, string clientId, string clientSecret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(clientSecret));
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(username + clientId));
        return Convert.ToBase64String(hash);
    }
}