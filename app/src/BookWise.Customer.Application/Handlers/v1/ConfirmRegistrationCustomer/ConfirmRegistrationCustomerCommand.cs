using BookWise.Customer.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Application.Handlers.v1.ConfirmRegistrationCustomer;

public sealed class ConfirmRegistrationCustomerCommand : IRequest<ConfirmRegistrationCustomerResult> 
{
    [FromBody]
    public PayloadConfirmRegistrationCustomer? Payload { get; set; }
}