using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Handlers.v1.Create;

public sealed class CreateCustomerResult
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
}
