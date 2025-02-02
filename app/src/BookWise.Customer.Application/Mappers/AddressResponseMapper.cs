using System.Text.Json.Nodes;
using Amazon.CognitoIdentityProvider.Model;
using AutoMapper;
using BookWise.Customer.Application.Models.Responses;
using BookWise.Customer.Domain.ValueObjects;
using Newtonsoft.Json;
using DomainEntity = BookWise.Customer.Domain.Entities;

namespace BookWise.Customer.Application.Mappers;

public class AddressResponseMapper : IValueConverter<AdminGetUserResponse, CustomerAddressResponse>
{
    public CustomerAddressResponse Convert(DomainEntity.Customer sourceMember, ResolutionContext context)
    {
        return new CustomerAddressResponse()
        {
            City = sourceMember.Address!.City,
            Number = sourceMember.Address.Number,
            State = sourceMember.Address.State,
            ZipCode = sourceMember.Address.ZipCode,
            Street = sourceMember.Address.Street,
        };
    }

    public CustomerAddressResponse Convert(AdminGetUserResponse sourceMember, ResolutionContext context)
    {
        var jsonString = sourceMember.UserAttributes.Find(x => x.Name == "address")!.Value;
        
        var address = JsonConvert.DeserializeObject<CustomerAddress>(jsonString);

        if (address is not null)
        {
            return new CustomerAddressResponse()
            {
                City = address.City,
                Number = address.Number,
                State = address.State,
                ZipCode = address.ZipCode,
                Street = address.Street,
            };
            
        }
        
        return new CustomerAddressResponse();
    }
}