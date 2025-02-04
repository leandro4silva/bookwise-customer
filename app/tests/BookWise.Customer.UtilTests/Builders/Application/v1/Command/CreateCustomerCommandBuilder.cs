using Bogus;
using BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;
using BookWise.Customer.Application.Models.Requests;
using BookWise.Customer.UtilTests.Builders.Base;

namespace BookWise.Customer.UtilTests.Builders.Application.v1.Command;

public sealed class CreateCustomerCommandBuilder : LazyFakerBuilder<RegistrationCustomerCommand>
{
    public static CreateCustomerCommandBuilder Instance { get; } = new();

    protected override Faker<RegistrationCustomerCommand> Factory()
    {
        var address = new Faker<AddressRequest>()
            .RuleFor(op => op.City, setter => setter.Address.City())
            .RuleFor(op => op.Number, setter => setter.Address.BuildingNumber())
            .RuleFor(op => op.State, setter => setter.Address.State())
            .RuleFor(op => op.Street, setter => setter.Address.StreetAddress())
            .RuleFor(op => op.ZipCode, setter => setter.Address.ZipCode());

        var payload = new Faker<PayloadCreateCustomer>()
            .RuleFor(op => op.Email, setter => setter.Person.Email)
            .RuleFor(op => op.BirthDate, setter => setter.Person.DateOfBirth)
            .RuleFor(op => op.FullName, setter => setter.Person.FullName)
            .RuleFor(op => op.PhoneNumber, setter => setter.Person.Phone)
            .RuleFor(op => op.Address, _ => address.Generate());

        return new Faker<RegistrationCustomerCommand>("pt_BR")
            .RuleFor(op => op.Payload, _ => payload.Generate());
    }
}
