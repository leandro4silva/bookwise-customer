using BookWise.Customer.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;

public sealed class RegistrationCustomerCommand : IRequest<RegistrationCustomerResult>
{
    [FromBody]
    public PayloadCreateCustomer? Payload { get; set; }
}