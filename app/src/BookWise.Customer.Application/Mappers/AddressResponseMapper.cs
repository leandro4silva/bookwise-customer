using AutoMapper;
using BookWise.Customer.Application.Models.Responses;
using DomainEntity = BookWise.Customer.Domain.Entities;

namespace BookWise.Customer.Application.Mappers;

public class AddressResponseMapper : IValueConverter<DomainEntity.Customer, CustomerAddressResponse>
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
}