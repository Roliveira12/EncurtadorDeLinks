using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IShortUrlRepository
    {
        Task CreateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default);

        Task GetByIdAsync(string id);

        Task<ShortenedUrl> GetByLongUriAsync(string longUri);

        Task<IEnumerable<ShortenedUrl>> GetAllAsync();
    }
}