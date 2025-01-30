namespace BookWise.Customer.Infrastructure.Auths.Dtos;

public sealed class CognitoConfig
{
    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }

    public string? PoolId { get; set; }
}