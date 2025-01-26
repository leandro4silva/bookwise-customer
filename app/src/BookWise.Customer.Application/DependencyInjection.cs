using BookWise.Customer.Application.Handlers.v1.Customer.Create;
using BookWise.Customer.Application.Handlers.v1.Customer.UpdateImage;
using BookWise.Customer.Application.Mappers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookWise.Customer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region MediatR

        services.AddMediatR(typeof(CreateCustomerHandler));
        services.AddAutoMapperProfiles();
        services.AddValidatorsFromAssembly(typeof(CreateCustomerValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateImageCustomerCommand).Assembly);

        #endregion

        return services;
    }

    private static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CustomerProfile));
        return services;
    }
}
