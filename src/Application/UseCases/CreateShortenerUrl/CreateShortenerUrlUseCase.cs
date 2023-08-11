using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using WebApi.Boundaries.ShortenerUrl;

namespace Application.UseCases.CreateShortenerUrl
{
    public class CreateShortenerUrlUseCase : ICreateShortenerUrlUseCase
    {
        private readonly IShortUrlRepository shortUrlRepository;

        public CreateShortenerUrlUseCase(IShortUrlRepository shortUrlRepository)
        {
            this.shortUrlRepository = shortUrlRepository;
        }

        public async Task<ShortenerUrlOutput> ExecuteAsync(string input, CancellationToken cancellationToken)
        {
            var validationResult = ValidateInput(input);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var alreadyExistsUri = await shortUrlRepository.GetByLongUriAsync(input);

            if (alreadyExistsUri is not null)
            {
                return new ShortenerUrlOutput();
            }

            //cria o id da url
            var shortUrlId = Guid.NewGuid().ToString().Substring(0, 6);
            var dbEntity = GetEntity(input, shortUrlId);

            //grava no banco

            await shortUrlRepository.CreateAsync(dbEntity, cancellationToken);
            
            //publica na fila do RabbitMQ a mensagem para consumo.


            //retona output.

            return new ShortenerUrlOutput();
        }

        private static ShortenedUrl GetEntity(string input, string shorterUrlId)
        {
            return new ShortenedUrl()
            {
                Id = Guid.NewGuid(),
                ShorterUrlId = shorterUrlId,
                OriginalUrl = input
            };
        }

        private static ValidationResult ValidateInput(string input)
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

            return new ValidationResult(validationFailures);
        }
    }
}