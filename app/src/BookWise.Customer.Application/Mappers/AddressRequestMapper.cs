using AutoMapper;
using BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;
using BookWise.Customer.Domain.ValueObjects;

namespace BookWise.Customer.Application.Mappers;

public sealed class AddressRequestMapper : IValueConverter<RegistrationCustomerCommand, CustomerAddress>
{
    public CustomerAddress Convert(RegistrationCustomerCommand sourceMember, ResolutionContext context)
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

