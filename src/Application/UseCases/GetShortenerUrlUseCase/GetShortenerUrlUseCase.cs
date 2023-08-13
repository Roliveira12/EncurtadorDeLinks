using Application.UseCases.Boundaries.ShortenerUrl;
using Application.UseCases.CreateShortenerUrl.Mappers;
using Application.UseCases.GetShortenerUrlUseCase.Enums;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;

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

            var shortenedUrlEntity = await shortUrlRepository.GetByShortIdAsync(input, cancellationtoken);

            if (shortenedUrlEntity != null)
            {
                shortenedUrlEntity.AccessCount++;
                await shortUrlRepository.UpdateAsync(shortenedUrlEntity, cancellationtoken);
                output = shortenedUrlEntity.MapToOutput();
            }

            return output;
        }

        public async Task<IEnumerable<ShortenerUrlOutput>> GetTopItemsAsync(int top, OrderByType orderByType, CancellationToken cancellationToken)
        {
            //Adiciona os top items do URL Json e adiciona o range de acordo com o banco de dados.

            IEnumerable<ShortenerUrlOutput> outputs = Enumerable.Empty<ShortenerUrlOutput>();
            var shortenedUrls = await shortUrlRepository.GetAllAsync(cancellationToken);

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