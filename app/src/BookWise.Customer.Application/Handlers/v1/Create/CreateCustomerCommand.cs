using BookWise.Customer.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookWise.Customer.Application.Handlers.v1.Create;

public sealed class CreateCustomerCommand : IRequest<CreateCustomerResult>
{
    [FromBody]
    public PayloadCreateCustomer? Payload { get; set; }
}