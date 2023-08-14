using Infrastructure.Bus.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Bus
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services)
        {
            //AddOptions
            services.AddOptions<RabbitMQConfigurations>()
                    .Configure<IConfiguration>((options, configuration) =>
                     {
                         configuration.GetSection("MessageBroker:RabbitMq").Bind(options);
                     });

            //RabbitMQ

            services.AddSingleton<IRabbitBus>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<RabbitMQConfigurations>>();

                return new RabbitBus(options);
            });

            return services;
        }
    }
}