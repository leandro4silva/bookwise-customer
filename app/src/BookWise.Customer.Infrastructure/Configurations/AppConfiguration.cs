using BookWise.Customer.Infrastructure.LogAudit.Dtos;
using System.Diagnostics.CodeAnalysis;
using Amazon.S3;
using BookWise.Customer.Infrastructure.Auths.Dtos;
using BookWise.Customer.Infrastructure.Buckets.Dtos;

namespace BookWise.Customer.Infrastructure.Configurations;

[ExcludeFromCodeCoverage]
public sealed class AppConfiguration
{
    private const string EnviromentDev = "dev";
    private const string EnvironmentHom = "hom";

    public CreateCustomerSqsConfig? CreateCustomerSqsConfig { get; set; }

    public AuditoriaConfig? AuditoriaConfig { get; set; }

    public UserImageConfig? UserImage { get; set; }

    public AwsS3Config? AwsS3Config { get; set; }

    public CognitoConfig? CognitoConfig { get; set; }

    public string? Environment { get; set; }
    
    public bool IsDevelopment =>
        EnviromentDev.Equals(Environment, StringComparison.OrdinalIgnoreCase);

    public bool IsStaging =>
        EnvironmentHom.Equals(Environment, StringComparison.OrdinalIgnoreCase);
}
