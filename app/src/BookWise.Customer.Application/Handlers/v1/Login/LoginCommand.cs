using BookWise.Customer.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Application.Handlers.v1.Login;

public sealed class LoginCommand : IRequest<LoginResult>
{
    [FromBody]
    public PayloadLogin? Payload { get; set; }
}