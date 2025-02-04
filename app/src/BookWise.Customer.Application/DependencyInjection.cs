using BookWise.Customer.Application.Common;
using BookWise.Customer.Application.Handlers.v1.RegistrationCustomer;
using BookWise.Customer.Application.Mappers;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookWise.Customer.Application;

public static class DependencyInjection
{
    [Obsolete("Obsolete")]
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        #region MediatR

        services.AddMediatR(typeof(RegistrationCustomerHandler));
        services.AddAutoMapperProfiles();
        
        services.AddValidatorsFromAssembly(typeof(RegistrationCustomerValidator).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        #endregion

        return services;
    }

    private static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CustomerProfile));
        return services;
    }
}
