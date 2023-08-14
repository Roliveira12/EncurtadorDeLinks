using Domain.Entities;
using Domain.Entities.Enums;

namespace Domain.Interfaces
{
    public interface IShortUrlRepository
    {
        Task CreateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default);

        Task<ShortenedUrl?> GetByShortIdAsync(string id, CancellationToken cancellationtoken = default);

        Task<ShortenedUrl?> GetByLongUrlAsync(string longUri, CancellationToken cancellationToken = default);

        Task UpdateAsync(ShortenedUrl enitity, CancellationToken cancellationToken = default);

        Task<IEnumerable<ShortenedUrl>> GetTopItemsAsync(int top, OrderByType orderByType, CancellationToken cancellationToken = default);
    }
}