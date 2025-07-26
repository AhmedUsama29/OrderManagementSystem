using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;


namespace Persistence
{
    public static class InfrastructureServicesRegisteration
    {

        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configurations)
        {

            services.AddDbContext<OrderManagementDbContext>(options =>
                            options.UseSqlServer(configurations.GetConnectionString("OrderManagementConnection")));


            return services;
        }


    }
}
