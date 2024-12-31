using BookWise.Customer.Application.Models.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Handlers.v1.Customer.Create;

public sealed class CreateCustomerCommand : IRequest<CreateCustomerResult>
{
    [FromBody]
    public Payload? Payload { get; set; }
}

public sealed class Payload
{
    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("address")]
    public AddressRequest? Address { get; set; }
}