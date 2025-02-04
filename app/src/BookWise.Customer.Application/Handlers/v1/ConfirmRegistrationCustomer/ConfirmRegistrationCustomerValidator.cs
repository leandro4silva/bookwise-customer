using FluentValidation;

namespace BookWise.Customer.Application.Handlers.v1.ConfirmRegistrationCustomer;

public sealed class ConfirmRegistrationCustomerValidator : AbstractValidator<ConfirmRegistrationCustomerCommand>
{
    public ConfirmRegistrationCustomerValidator()
    {
        RuleFor(x => x.Payload!.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .WithName("email");
        
        RuleFor(x => x.Payload!.ConfirmCode)
            .NotEmpty()
            .NotNull()
            .WithName("confirmCode");;
    }
}