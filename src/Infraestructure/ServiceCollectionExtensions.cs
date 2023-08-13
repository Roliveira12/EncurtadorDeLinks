using Infrastructure.Bus;
using Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDatabaseServices()
                .AddMessageBus()
                .AddRepositories();

            return services;
        }
    }
}