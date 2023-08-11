using Domain.Entities;
using WebApi.Boundaries.ShortenerUrl;

namespace Application.UseCases.CreateShortenerUrl.Mappers
{
    public static class ShortenerToOutputMapperExtensions
    {
        public static ShortenerUrlOutput MapToOutput(this ShortenedUrl shortenedUrl)
        {
            return new ShortenerUrlOutput()
            {
                AcessCount = shortenedUrl.AccessCount,
                Id = shortenedUrl.Id.ToString(),
                OriginalUrl = shortenedUrl.OriginalUrl,
                ShortUrl = shortenedUrl.ShorterUrlId
            };
        }
    }
}