using Bogus;
using BookWise.Customer.Application.Handlers.v1.Create;
using BookWise.Customer.UtilTests.Builders.Base;

namespace BookWise.Customer.UtilTests.Builders.Application.v1.Result;

public sealed class CreateCustomerResultBuilder : LazyFakerBuilder<CreateCustomerResult>
{
    public static CreateCustomerResultBuilder Instance { get; } = new();


    protected override Faker<CreateCustomerResult> Factory()
    {
        return new Faker<CreateCustomerResult>("pt_BR")
            .RuleFor(op => op.Id, setter => setter.Random.Guid());
    }
}
