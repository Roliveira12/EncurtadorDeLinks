using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    internal sealed class ShortUrlRepository : IShortUrlRepository
    {
        private readonly ApplicationDbContext context;

        public ShortUrlRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task CreateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default)
        {
            await context.ShortUrls.AddAsync(entity, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<ShortenedUrl>> GetTopItemsAsync(int top, OrderByType orderByType, CancellationToken cancellationToken = default)
        {
            if (orderByType == OrderByType.Ascending)
            {
                return await context.ShortUrls
                                    .Take(top)
                                    .OrderBy(x => x.Hits)
                                    .ToListAsync();
            }
            else
            {
                return await context.ShortUrls
                    .Take(top)
                    .OrderByDescending(x => x.Hits)
                    .ToListAsync();
            }
        }

        public async Task<ShortenedUrl?> GetByShortIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await context.ShortUrls.FirstOrDefaultAsync(x => x.ShortUrl == id, cancellationToken);

            return result;
        }

        public async Task<ShortenedUrl?> GetByLongUrlAsync(string originalUrl, CancellationToken cancellationToken = default)
        {
            var result = await context.ShortUrls
                        .FirstOrDefaultAsync(x => x.Url == originalUrl, cancellationToken);

            return result;
        }

        public async Task UpdateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}