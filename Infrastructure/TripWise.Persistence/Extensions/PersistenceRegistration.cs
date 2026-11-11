using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TripWise.Application.Contracts.Persistence;
using TripWise.Persistence.Repositories;

namespace TripWise.Persistence.Extensions
{
    public static  class PersistenceRegistration
    {
        public static IServiceCollection AddPersistenceExt(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });
            services.AddScoped<ITripRepository, TripRepository>();
            return services;
        }
    }
}
