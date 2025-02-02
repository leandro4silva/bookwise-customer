using AutoMapper;
using BookWise.Customer.Application.Handlers.v1.Create;
using BookWise.Customer.Domain.ValueObjects;

namespace BookWise.Customer.Application.Mappers;

public sealed class AddressRequestMapper : IValueConverter<CreateCustomerCommand, CustomerAddress>
{
    public CustomerAddress Convert(CreateCustomerCommand sourceMember, ResolutionContext context)
    {
        return new CustomerAddress()
        {
            City = sourceMember.Payload!.Address!.City,
            Number = sourceMember.Payload!.Address!.Number,
            State = sourceMember.Payload!.Address!.State,
            ZipCode = sourceMember.Payload!.Address!.ZipCode,
            Street = sourceMember.Payload!.Address!.Street,
        };
    }
}

