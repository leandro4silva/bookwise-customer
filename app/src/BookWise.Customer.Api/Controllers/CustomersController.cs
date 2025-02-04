using BookWise.Customer.Application.Common.Models;
using BookWise.Customer.Application.Handlers.v1.ConfirmRegistrationCustomer;
using BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;
using BookWise.Customer.Application.Handlers.v1.UpdateImageCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Api.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/customers")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<RegistrationCustomerResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> RegisterCustomer(
        RegistrationCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return CreatedAtAction(nameof(RegisterCustomer), response);
    }
    
    [HttpPost("confirm-registration")]
    [ProducesResponseType(typeof(BaseResponse<ConfirmRegistrationCustomerResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConfirmRegistration(
        ConfirmRegistrationCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPatch("image")]
    [ProducesResponseType(typeof(BaseResponse<UpdateImageCustomerResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCustomerImage(
        UpdateImageCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
