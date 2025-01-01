using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Application.Handlers.v1.Customer.UpdateImage;

public sealed class UpdateImageCustomerCommand : IRequest<UpdateImageCustomerResult>
{
    [FromRoute(Name = "id")]
    public Guid Id { get; set; }

    [FromForm(Name = "image")]
    public IFormFile? Image { get; set; }
}
