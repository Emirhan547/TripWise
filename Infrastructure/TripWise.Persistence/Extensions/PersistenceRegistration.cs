using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TripWise.Application.Abstractions.Identity;
using TripWise.Application.Contracts.Persistence;
using TripWise.Persistence.Identity;
using TripWise.Persistence.Repositories;

namespace TripWise.Persistence.Extensions;

public static class PersistenceRegistration
{
    public static IServiceCollection AddPersistenceExt(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("SqlServer"));
        });

        services.AddScoped<ITripRepository, TripRepository>();
        services.AddScoped<IDestinationRepository, DestinationRepository>();
        services.AddScoped<ITripDestinationRepository, TripDestinationRepository>();
        services.AddIdentityCore<ApplicationUser>(options =>
        {
            options.User.RequireUniqueEmail = true;

            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = false;
        })
        .AddRoles<IdentityRole<Guid>>()
        .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddOptions<JwtOptions>()
            .Bind(configuration.GetSection(JwtOptions.SectionName))
            .Validate(
                x => !string.IsNullOrWhiteSpace(x.Issuer),
                "JWT Issuer belirtilmelidir.")
            .Validate(
                x => !string.IsNullOrWhiteSpace(x.Audience),
                "JWT Audience belirtilmelidir.")
            .Validate(
                x =>
                {
                    try
                    {
                        return Convert.FromBase64String(x.Secret).Length >= 32;
                    }
                    catch (FormatException)
                    {
                        return false;
                    }
                },
                "JWT Secret geçerli Base64 formatında ve en az 32 byte olmalıdır.")
            .Validate(
                x => x.AccessTokenExpirationMinutes > 0,
                "JWT geçerlilik süresi sıfırdan büyük olmalıdır.")
            .ValidateOnStart();

        return services;
    }
}