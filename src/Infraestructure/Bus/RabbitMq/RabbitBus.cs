using EasyNetQ;
using Microsoft.Extensions.Options;
using System.Text;

namespace Infrastructure.Bus.RabbitMq
{
    internal sealed class RabbitBus : IRabbitBus
    {
        private readonly IAdvancedBus advancedBus;
        private readonly IOptionsSnapshot<RabbitMQConfigurations> rabbitConfigurations;

        public RabbitBus(IOptionsSnapshot<RabbitMQConfigurations> rabbitConfigurations)
        {
            this.rabbitConfigurations = rabbitConfigurations;
            advancedBus = RabbitHutch.CreateBus(rabbitConfigurations.Value.ConnectionString).Advanced;
        }

        public async Task PublishAsync<TMessage>(TMessage message, string exchangeName, string routingKey, ExchangeType exchangeType)
        {
            var exchange = await advancedBus.ExchangeDeclareAsync(exchangeName, x => x.WithType(Enum.GetName(typeof(ExchangeType), exchangeType)));
            var messageBody = Encoding.UTF8.GetBytes(System.Text.Json.JsonSerializer.Serialize(message));

            var properties = new MessageProperties
            {
                ContentType = "application/json"
            };
            var body = new Message<byte[]>(messageBody);

            await advancedBus.PublishAsync(exchange, routingKey, false, properties, messageBody);
        }
    }
}