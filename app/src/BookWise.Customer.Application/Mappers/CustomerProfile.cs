using AutoMapper;
using BookWise.Customer.Application.Handlers.v1.Customer.Create;
using BookWise.Customer.Application.Handlers.v1.Customer.UpdateImage;
using BookWise.Customer.Domain.Events;
using DomainEntity = BookWise.Customer.Domain.Entities;

namespace BookWise.Customer.Application.Mappers;

public sealed class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        _ = CreateMap<CreateCustomerCommand, DomainEntity.Customer>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Payload!.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Payload!.FullName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.Payload!.BirthDate))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Payload!.PhoneNumber))
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Payload!.Password))
            .ForMember(dest => dest.Address, opt => opt.ConvertUsing(new AddressRequestMapper(), src => src));

        _ = CreateMap<DomainEntity.Customer, CustomerCreated>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName));

        _ = CreateMap<DomainEntity.Customer, CreateCustomerResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        _ = CreateMap<DomainEntity.Customer, UpdateImageCustomerResult>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.ConvertUsing(new AddressResponseMapper(), src => src));
    }
}
