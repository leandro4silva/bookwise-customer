using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Application.Handlers.v1.UpdateImageCustomer;

public sealed class UpdateImageCustomerCommand : IRequest<UpdateImageCustomerResult>
{
    [FromForm(Name = "email")]
    public string? Email { get; set; }

    [FromForm(Name = "image")]
    public IFormFile? Image { get; set; }
}
