using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IShortUrlRepository
    {
        Task CreateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default);

        Task<ShortenedUrl?> GetByShortIdAsync(string id, CancellationToken cancellationtoken = default);

        Task<ShortenedUrl?> GetByLongUrlAsync(string longUri, CancellationToken cancellationToken = default);

        Task UpdateAsync(ShortenedUrl enitity, CancellationToken cancellationToken = default);

        Task<IEnumerable<ShortenedUrl>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}