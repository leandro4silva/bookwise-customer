using Bogus;
using BookWise.Customer.Domain.Events;
using BookWise.Customer.UtilTests.Builders.Base;

namespace BookWise.Customer.UtilTests.Builders.Domain;

public sealed class CustomerCreatedBuilder : LazyFakerBuilder<CustomerCreated>
{
    public static CustomerCreatedBuilder Instance { get; } = new();

    protected override Faker<CustomerCreated> Factory()
    {
        return new Faker<CustomerCreated>("pt_BR")
            .RuleFor(op => op.Email, setter => setter.Person.Email)
            .RuleFor(op => op.FullName, setter => setter.Person.FullName)
            .RuleFor(op => op.Id, setter => setter.Random.Guid());
    }
}
