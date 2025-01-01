using Bogus;
using BookWise.Customer.Domain.ValueObjects;
using BookWise.Customer.UtilTests.Builders.Base;
using DomainEntity = BookWise.Customer.Domain.Entities;

namespace BookWise.Customer.UtilTests.Builders.Domain;

public sealed class CustomerBuilder : LazyFakerBuilder<DomainEntity.Customer>
{
    public static CustomerBuilder Instance { get; } = new();

    protected override Faker<DomainEntity.Customer> Factory()
    {
        var address = new Faker<CustomerAddress>()
            .RuleFor(op => op.City, setter => setter.Address.City())
            .RuleFor(op => op.Number, setter => setter.Address.BuildingNumber())
            .RuleFor(op => op.State, setter => setter.Address.State())
            .RuleFor(op => op.Street, setter => setter.Address.StreetAddress())
            .RuleFor(op => op.ZipCode, setter => setter.Address.ZipCode());

        return new Faker<DomainEntity.Customer>("pt_BR")
            .RuleFor(op => op.Email, setter => setter.Person.Email)
            .RuleFor(op => op.BirthDate, setter => setter.Person.DateOfBirth)
            .RuleFor(op => op.FullName, setter => setter.Person.FullName)
            .RuleFor(op => op.PhoneNumber, setter => setter.Person.Phone)
            .RuleFor(op => op.Address, _ => address.Generate());
    }
}
