﻿using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Models.Requests;

public sealed class PayloadCreateCustomer
{
    [JsonPropertyName("fullName")]
    public string? FullName { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("birthDate")]
    public DateTime BirthDate { get; set; }
    
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("address")]
    public AddressRequest? Address { get; set; }
}