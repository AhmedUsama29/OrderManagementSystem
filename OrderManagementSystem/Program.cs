using Persistence;
using Services;
using TaskHive.Middelwares;

namespace OrderManagementSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddInfrastructureRegistration(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);
            builder.Services.AddAplicationServices(builder.Configuration);

            var app = builder.Build();

            await app.InitializeDbAsync();

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "Order Management";
                    options.EnableFilter();
                    options.DisplayRequestDuration();
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
