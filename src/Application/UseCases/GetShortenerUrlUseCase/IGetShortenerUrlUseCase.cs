using Application.UseCases.Boundaries.ShortenerUrl;
using Domain.Entities.Enums;

namespace Application.UseCases.GetShortenerUrlUseCase
{
    public interface IGetShortenerUrlUseCase : IUseCase<string, ShortenerUrlOutput>
    {
        Task<IEnumerable<ShortenerUrlOutput>> GetTopItemsAsync(int top, OrderByType orderByType, CancellationToken cancellationToken);
    }
}