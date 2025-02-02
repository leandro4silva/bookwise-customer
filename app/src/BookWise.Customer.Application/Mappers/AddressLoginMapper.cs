using Amazon.CognitoIdentityProvider.Model;
using AutoMapper;
using BookWise.Customer.Domain.ValueObjects;
using Newtonsoft.Json;

namespace BookWise.Customer.Application.Mappers;

public class AddressLoginMapper : IValueConverter<AdminGetUserResponse, CustomerAddress>
{
    public CustomerAddress Convert(AdminGetUserResponse sourceMember, ResolutionContext context)
    {
        var jsonString = sourceMember.UserAttributes.Find(x => x.Name == "address")!.Value;
        
        var address = JsonConvert.DeserializeObject<CustomerAddress>(jsonString);

        return address ?? new CustomerAddress();
    }
}