using Domain.Entities;
using WebApi.Boundaries.ShortenerUrl;

namespace Application.UseCases
{
    public interface IGetShortenerUrlUseCase : IUseCase<string, ShortenerUrlOutput>
    {
        Task<ShortenerUrlOutput> GetByIdAsync(string shortenerUrlId);
        Task<List<ShortenerUrlOutput>> GetAllAsync();

    }
}