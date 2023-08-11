using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IShortUrlRepository
    {
        Task CreateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default);

        Task<ShortenedUrl?> GetByIdAsync(string id, CancellationToken cancellationtoken = default);

        Task<ShortenedUrl?> GetByLongUriAsync(string longUri, CancellationToken cancellationToken = default);

        Task UpdateAsync(ShortenedUrl enitity, CancellationToken cancellationToken = default);

        Task<IEnumerable<ShortenedUrl>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}