using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;

public sealed class RegistrationCustomerResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}
