using Domain.Interfaces;
using Infraestructure.Database;
using Infrastructure.Database.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Database
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
        {
            services.AddOptions<PostgreConfigurations>()
                    .Configure<IConfiguration>((options, configuration) =>
                    {
                        configuration.GetSection("DatabaseSettings:Postgres").Bind(options);
                    });

            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var configs = serviceProvider.GetService<IOptionsSnapshot<PostgreConfigurations>>().Value;
                options.UseNpgsql(configs.GetConnectionString());
            });

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {

            services.AddScoped<IShortUrlRepository, ShortUrlRepository>();

            return services;
        }
    }
}