using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Handlers.v1.ConfirmRegistrationCustomer;

public sealed class ConfirmRegistrationCustomerResult
{
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}