using FluentValidation;

namespace BookWise.Customer.Application.Handlers.v1.Customer.Create;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Payload!.Email)
            .NotEmpty()
            .NotNull()
            .WithName("email");

        RuleFor(x => x.Payload!.FullName)
            .NotEmpty()
            .NotNull()
            .WithName("fullName");

        RuleFor(x => x.Payload!.BirthDate)
            .NotEmpty()
            .NotNull()
            .WithName("birthDate");

        RuleFor(x => x.Payload!.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .WithName("phoneNumber");

        RuleFor(x => x.Payload!.Address)
            .NotEmpty()
            .NotNull()
            .WithName("address");

        RuleFor(x => x.Payload!.Address!.City)
            .NotEmpty()
            .NotNull()
            .WithName("city");

        RuleFor(x => x.Payload!.Address!.Number)
            .NotEmpty()
            .NotNull()
            .WithName("number");

        RuleFor(x => x.Payload!.Address!.Street)
            .NotEmpty()
            .NotNull()
            .WithName("street");

        RuleFor(x => x.Payload!.Address!.State)
            .NotEmpty()
            .NotNull()
            .WithName("state");

        RuleFor(x => x.Payload!.Address!.ZipCode)
            .NotEmpty()
            .NotNull()
            .WithName("zipCode");
    }
}
