using BookWise.Customer.Application.Common.Models;
using BookWise.Customer.Application.Handlers.v1.LoginCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Api.Controllers;

[ApiVersion("1.0")]
[Route("v{version:apiVersion}/auth")]
[ApiController]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(BaseResponse<LoginCustomerResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Post(
        LoginCustomerCommand request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}