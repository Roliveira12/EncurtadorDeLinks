namespace Infrastructure.Bus.RabbitMq
{
    public enum ExchangeType
    {
        direct,

        fanout,

        headers,

        topic,
    }
}