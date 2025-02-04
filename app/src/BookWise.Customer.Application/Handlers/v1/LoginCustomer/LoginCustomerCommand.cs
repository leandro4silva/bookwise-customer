using BookWise.Customer.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Application.Handlers.v1.LoginCustomer;

public sealed class LoginCustomerCommand : IRequest<LoginCustomerResult>
{
    [FromBody]
    public PayloadLogin? Payload { get; set; }
}