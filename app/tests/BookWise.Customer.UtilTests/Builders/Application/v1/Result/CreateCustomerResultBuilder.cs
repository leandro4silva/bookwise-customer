using Bogus;
using BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;
using BookWise.Customer.UtilTests.Builders.Base;

namespace BookWise.Customer.UtilTests.Builders.Application.v1.Result;

public sealed class CreateCustomerResultBuilder : LazyFakerBuilder<RegistrationCustomerResult>
{
    public static CreateCustomerResultBuilder Instance { get; } = new();


    protected override Faker<RegistrationCustomerResult> Factory()
    {
        return new Faker<RegistrationCustomerResult>("pt_BR")
            .RuleFor(op => op.Id, setter => setter.Random.Guid());
    }
}
