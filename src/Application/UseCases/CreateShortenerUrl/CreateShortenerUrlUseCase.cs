using Application.UseCases.Boundaries.ShortenerUrl;
using Application.UseCases.CreateShortenerUrl.Mappers;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Bus.RabbitMq;

namespace Application.UseCases.CreateShortenerUrl
{
    public class CreateShortenerUrlUseCase : ICreateShortenerUrlUseCase
    {
        private readonly IShortUrlRepository shortUrlRepository;
        private readonly IRabbitBus bus;

        private const string PublishExchangeName = "shorted.url";
        private const string RoutingKey = "on.shorted.url.created";

        public CreateShortenerUrlUseCase(IShortUrlRepository shortUrlRepository, IRabbitBus bus)
        {
            this.shortUrlRepository = shortUrlRepository;
            this.bus = bus;
        }

        public async Task<ShortenerUrlOutput?> ExecuteAsync(string input, CancellationToken cancellationToken)
        {
            ShortenerUrlOutput output = new();
            ValidateInput(input);

            var existingUri = await shortUrlRepository.GetByLongUrlAsync(input, cancellationToken);

            if (existingUri is not null)
            {
                output = existingUri.MapToOutput();
                return output;
            }

            //cria o id da url
            var shortUrlId = Guid.NewGuid().ToString().Substring(0, 6);
            var dbEntity = GetEntity(input, shortUrlId);

            //grava no banco
            await shortUrlRepository.CreateAsync(dbEntity, cancellationToken);

            //publica na fila do RabbitMQ a mensagem para consumo.

            _ = PublishMessage(dbEntity);

            output = dbEntity.MapToOutput();

            return output;
        }

        private async Task PublishMessage(ShortenedUrl @event)
        {
            await bus.PublishAsync(@event, PublishExchangeName, RoutingKey, ExchangeType.topic);
        }

        private static ShortenedUrl GetEntity(string input, string shorterUrlId)
        {
            return new ShortenedUrl()
            {
                ShorterUrlId = shorterUrlId,
                OriginalUrl = input,
                CreatedDate = DateTime.UtcNow,
                
            };
        }

        private static void ValidateInput(string input)
        {
            var validationFailures = new List<ValidationFailure>();
            if (string.IsNullOrEmpty(input))
            {
                validationFailures.Add(new ValidationFailure(nameof(input), "input cannot be null"));
            }

            var isValidUrl = Uri.IsWellFormedUriString(input, UriKind.Absolute);
            if (!isValidUrl)
            {
                validationFailures.Add(new ValidationFailure(nameof(input), "Is not a valid Uri"));
            }

            if (validationFailures.Any()) 
            {
                throw new ValidationException(validationFailures);
            }

        }
    }
}