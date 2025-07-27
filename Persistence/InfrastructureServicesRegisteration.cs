using Domain.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;


namespace Persistence
{
    public static class InfrastructureServicesRegisteration
    {

        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configurations)
        {

            services.AddDbContext<OrderManagementDbContext>(options =>
                            options.UseSqlServer(configurations.GetConnectionString("OrderManagementConnection")));

            services.AddDbContext<OrderManagementIdentityDbContext>(options =>
                           options.UseSqlServer(configurations.GetConnectionString("OrderManagementIdentityConnection")));

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.RegisterIdentity();

            return services;
        }

        private static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {

            services.AddIdentityCore<IdentityUser>(config =>
            {
                config.User.RequireUniqueEmail = false;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<OrderManagementIdentityDbContext>();

            return services;
        }

    }
}
