﻿using System.Text.Json.Serialization;

namespace BookWise.Customer.Application.Handlers.v1.LoginCustomer;

public sealed class LoginCustomerResult 
{
    [JsonPropertyName("accessToken")]
    public string? AccessToken { get; set; }
    
    [JsonPropertyName("expiresIn")]
    public int? ExpiresIn { get; set; }
    
    [JsonPropertyName("idToken")]
    public string? IdToken { get; set; }
    
    [JsonPropertyName("refreshToken")]
    public string? RefreshToken { get; set; }
    
    [JsonPropertyName("tokenType")]
    public string? TokenType { get; set; }
}