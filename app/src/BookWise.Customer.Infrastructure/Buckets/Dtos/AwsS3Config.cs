using System.Diagnostics.CodeAnalysis;

namespace BookWise.Customer.Infrastructure.Buckets.Dtos;

[ExcludeFromCodeCoverage]
public class AwsS3Config
{
    public string? BucketName { get; set; }
}
