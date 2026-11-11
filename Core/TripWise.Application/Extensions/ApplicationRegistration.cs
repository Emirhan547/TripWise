using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TripWise.Application.Behaviors;

namespace TripWise.Application.Extensions;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationExt(this IServiceCollection services)
    {
        var assembly = typeof(ApplicationRegistration).Assembly;

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);

            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}