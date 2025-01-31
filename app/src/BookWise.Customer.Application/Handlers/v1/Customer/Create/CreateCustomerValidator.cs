using FluentValidation;

namespace BookWise.Customer.Application.Handlers.v1.Customer.Create;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Payload!.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithName("email");

        RuleFor(x => x.Payload!.FullName)
            .NotEmpty()
            .NotNull()
            .WithName("fullName");
        
        RuleFor(x => x.Payload!.Password)
            .NotEmpty()
            .MinimumLength(8)
            .Matches(@"[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches(@"[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches(@"\d").WithMessage("A senha deve conter pelo menos um número.")
            .Matches(@"[\W_]").WithMessage("A senha deve conter pelo menos um caractere especial (!@#$%^&*).")
            .WithName("password");

        RuleFor(x => x.Payload!.BirthDate)
            .NotEmpty()
            .NotNull()
            .WithName("birthDate");

        RuleFor(x => x.Payload!.PhoneNumber)
            .NotEmpty()
            .NotNull()
            .Matches(@"^\+55\d{11}$")
            .WithMessage("O número de telefone deve estar no formato +5519999999999.")
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
