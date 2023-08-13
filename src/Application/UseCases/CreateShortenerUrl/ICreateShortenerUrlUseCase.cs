using Application.UseCases.Boundaries.ShortenerUrl;

namespace Application.UseCases.CreateShortenerUrl
{
    public interface ICreateShortenerUrlUseCase : IUseCase<string, ShortenerUrlOutput>
    {
    }
}