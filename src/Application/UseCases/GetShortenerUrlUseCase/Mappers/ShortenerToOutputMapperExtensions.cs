using Application.UseCases.Boundaries.ShortenerUrl;
using Domain.Entities;

namespace Application.UseCases.GetShortenerUrlUseCase.Mappers
{
    public static class ShortenerToOutputMapperExtensions
    {
        public static ShortenerUrlOutput MapToOutput(this ShortenedUrl shortenedUrl)
        {
            return new ShortenerUrlOutput()
            {
                AcessCount = shortenedUrl.AccessCount,
                Id = shortenedUrl.Id,
                Url = shortenedUrl.OriginalUrl,
                ShortUrl = shortenedUrl.ShorterUrlId
            };
        }
    }
}
