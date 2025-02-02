using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Models.Requests;

public sealed class PayloadLogin
{
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}