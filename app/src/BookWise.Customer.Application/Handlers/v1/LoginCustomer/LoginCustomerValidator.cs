using FluentValidation;

namespace BookWise.Customer.Application.Handlers.v1.LoginCustomer;

public sealed class LoginCustomerValidator : AbstractValidator<LoginCustomerCommand>
{
    public LoginCustomerValidator()
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