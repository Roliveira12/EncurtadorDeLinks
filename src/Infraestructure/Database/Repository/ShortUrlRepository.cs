using Domain.Entities;
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

        public async Task<IEnumerable<ShortenedUrl>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await context.ShortUrls.ToListAsync();
        }

        public async Task<ShortenedUrl?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
        {
            var result = await context.ShortUrls.FirstOrDefaultAsync(x => x.ShorterUrlId == id, cancellationToken);

            return result;
        }

        public async Task<ShortenedUrl?> GetByLongUriAsync(string originalUrl, CancellationToken cancellationToken = default)
        {
            var result = await context.ShortUrls
                        .FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl, cancellationToken);

            return result;
        }

        public async Task UpdateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);
        }
    }
}