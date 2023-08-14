using Application.UseCases.Boundaries.ShortenerUrl;
using Application.UseCases.CreateShortenerUrl.Mappers;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using System.Text.Json;

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
                shortenedUrlEntity.Hits++;
                await shortUrlRepository.UpdateAsync(shortenedUrlEntity, cancellationtoken);
                output = shortenedUrlEntity.MapToOutput();
            }

            return output;
        }

        public async Task<IEnumerable<ShortenerUrlOutput>> GetTopItemsAsync(int top, OrderByType orderByType, CancellationToken cancellationToken)
        {
            List<ShortenerUrlOutput> outputs = new();
            //Adiciona os top items do URL Json e adiciona ao range recuperado do banco de dados.

            var jsonUrls = GetJsonUrls();
            if (jsonUrls.Any())
            {
                outputs.AddRange(jsonUrls.Select(x => x.MapToOutput()));
            }
            var shortenedUrls = await shortUrlRepository.GetTopItemsAsync(top, orderByType, cancellationToken);

            if (shortenedUrls.Any())
            {
                outputs.AddRange(shortenedUrls.Select(x => x.MapToOutput()));
            }

            var orderedOutputs = OrderType(orderByType, outputs);

            return orderedOutputs.Take(top);
        }

        private static IEnumerable<ShortenerUrlOutput> OrderType(OrderByType orderByType, List<ShortenerUrlOutput> outputs)
        {
            if (orderByType == OrderByType.Ascending)
            {
                return outputs.OrderBy(x => x.Hits);
            }

            return outputs.OrderByDescending(x => x.Hits);
        }

        private static IEnumerable<ShortenedUrl> GetJsonUrls()
        {
            var jsonFilePath = Path.Combine(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory), "urls.json");

            using StreamReader stream = new(jsonFilePath);

            var json = stream.ReadToEnd();

            var shortenedUrls = JsonSerializer.Deserialize<IEnumerable<ShortenedUrl>>(json, new JsonSerializerOptions(JsonSerializerDefaults.Web)) ?? Enumerable.Empty<ShortenedUrl>();

            return shortenedUrls;
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