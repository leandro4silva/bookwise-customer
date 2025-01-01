using Bogus;
using BookWise.Customer.Infrastructure.Configurations;
using BookWise.Customer.UtilTests.Builders.Base;

namespace BookWise.Customer.UtilTests.Builders.Infrastructure;

public sealed class UserImageConfigBuilder : LazyFakerBuilder<UserImageConfig>
{
    public static UserImageConfigBuilder Instance { get; } = new();

    protected override Faker<UserImageConfig> Factory()
    {
        return new Faker<UserImageConfig>("pt_BR")
            .RuleFor(dest => dest.ImageDefaultUrl, setter => setter.Internet.Url());
    }
}
