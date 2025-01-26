using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Models.Responses;

public class CustomerAddressResponse
{
    [JsonPropertyName("street")]
    public string? Street { get; set; }
    
    [JsonPropertyName("number")]
    public string? Number { get; set; }
    
    [JsonPropertyName("city")]
    public string? City { get; set; }
    
    [JsonPropertyName("state")]
    public string? State { get; set; }
    
    [JsonPropertyName("zipCode")]
    public string? ZipCode { get; set; }

}