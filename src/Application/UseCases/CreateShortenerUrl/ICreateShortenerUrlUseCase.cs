using Domain.Entities;
using WebApi.Boundaries.ShortenerUrl;

namespace Application.UseCases.CreateShortenerUrl
{
    public interface ICreateShortenerUrlUseCase : IUseCase<string, ShortenerUrlOutput>
    {
    }
}