using Application.UseCases.CreateShortenerUrl.Mappers;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using WebApi.Boundaries.ShortenerUrl;

namespace Application.UseCases.GetShortenerUrlUseCase
{
    internal sealed class GetShortenerUrlUseCase : IGetShortenerUrlUseCase
    {
        private readonly IShortUrlRepository shortUrlRepository;

        public GetShortenerUrlUseCase(IShortUrlRepository shortUrlRepository)
        {
            this.shortUrlRepository = shortUrlRepository;
        }

        public async Task<ShortenerUrlOutput?> ExecuteAsync(string input, CancellationToken cancellationtoken = default)
        {
            ShortenerUrlOutput? output = null;
            ValidateInput(input);

            var shortenedUrlEntity = await shortUrlRepository.GetByIdAsync(input, cancellationtoken);

            if (shortenedUrlEntity != null)
            {
                output = shortenedUrlEntity.MapToOutput();
            }

            return output;
        }

        public async Task<IEnumerable<ShortenerUrlOutput>> GetAllAsync()
        {
            IEnumerable<ShortenerUrlOutput> outputs = Enumerable.Empty<ShortenerUrlOutput>();
            var shortenedUrls = await shortUrlRepository.GetAllAsync();

            if (shortenedUrls.Any())
            {
                outputs = shortenedUrls.Select(x => x.MapToOutput());
            }

            return outputs;
        }

        private static void ValidateInput(string input)
        {
            var validationFailures = new List<ValidationFailure>();
            if (string.IsNullOrEmpty(input))
            {
                validationFailures.Add(new ValidationFailure(nameof(input), "input cannot be null"));
            }

            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }
        }


    }
}
