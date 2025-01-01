using Bogus;
using BookWise.Customer.Infrastructure.Configurations;
using BookWise.Customer.UtilTests.Builders.Base;

namespace BookWise.Customer.UtilTests.Builders.Infrastructure;

public sealed class CreateCustomerSqsConfigBuilder : LazyFakerBuilder<CreateCustomerSqsConfig>
{
    public static CreateCustomerSqsConfigBuilder Instance { get; } = new();

    protected override Faker<CreateCustomerSqsConfig> Factory()
    {
        return new Faker<CreateCustomerSqsConfig>("pt_BR")
            .RuleFor(dest => dest.SqsQueueUrl, setter => setter.Internet.Url())
            .RuleFor(dest => dest.Region, _ => "");
    }
}
