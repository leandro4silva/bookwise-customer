namespace BookWise.Customer.Infrastructure.Configurations;
public sealed class CreateCustomerSqsConfig
{
    public string? Region { get; set; }
    public string? SqsQueueUrl { get; set; }
}
