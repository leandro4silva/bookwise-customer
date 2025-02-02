using FluentValidation;

namespace BookWise.Customer.Application.Handlers.v1.Login;

public sealed class LoginValidator : AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Payload!.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();
        
        RuleFor(x => x.Payload!.Password)
            .NotEmpty()
            .NotNull();
    }
}