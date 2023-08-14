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
                Hits = shortenedUrl.Hits,
                Id = shortenedUrl.Id.ToString(),
                Url = shortenedUrl.Url,
                ShortUrl = shortenedUrl.ShortUrl
            };
        }
    }
}
