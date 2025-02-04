using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Models.Requests;

public sealed class PayloadConfirmRegistrationCustomer
{
    [JsonPropertyName("email")] 
    public string? Email { get; set; }
    
    [JsonPropertyName("confirmCode")]
    public string? ConfirmCode { get; set; }
}