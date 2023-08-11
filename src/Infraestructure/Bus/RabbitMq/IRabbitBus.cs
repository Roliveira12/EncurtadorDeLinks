using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Bus.RabbitMq
{
    public interface IRabbitBus
    {
        Task PublishAsync<T>(T message, string exchange, string routingKey, ExchangeType exchangeType);
    }
}
