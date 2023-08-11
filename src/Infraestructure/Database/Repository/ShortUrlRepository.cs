using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.Repository
{
    internal sealed class ShortUrlRepository : IShortUrlRepository
    {
        public Task CreateAsync(ShortenedUrl entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ShortenedUrl>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ShortenedUrl> GetByLongUriAsync(string longUri)
        {
            throw new NotImplementedException();
        }
    }
}
