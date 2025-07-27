using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstraction;
using Shared.Authentication;
namespace Services
{
    public static class ApplicationServiceRegisteration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services,
                                                               IConfiguration configuration)
        {

            services.AddAutoMapper(typeof(OrderProfile).Assembly);

            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();


            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<Func<IAuthenticationService>>(provider => ()
            => provider.GetRequiredService<IAuthenticationService>());

            services.AddScoped<Func<IOrderService>>(provider => ()
            => provider.GetRequiredService<IOrderService>());

            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));

            return services;
        }
    }
}
