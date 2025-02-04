using FluentValidation;

namespace BookWise.Customer.Application.Handlers.v1.UpdateImageCustomer;

public class UpdateImageCustomerValidator : AbstractValidator<UpdateImageCustomerCommand>
{
    public UpdateImageCustomerValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O campo 'email' não pode estar vazio.");
        
        RuleFor(x => x.Image)
            .NotNull()
            .WithMessage("A imagem é obrigatória.")
            .Must(file => file?.Length > 0)
            .WithMessage("O arquivo de imagem não pode estar vazio.")
            .Must(file => file?.ContentType.StartsWith("image/") ?? false)
            .WithMessage("O arquivo enviado não é uma imagem válida.");
    }
}