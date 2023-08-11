namespace Application.UseCases
{
    public interface IUseCase<TInput, TOutput> where TOutput : class
    {
        Task<TOutput> ExecuteAsync(TInput input, CancellationToken cancellationtoken = default);
    }

    public interface IUseCase
    {
        Task ExecuteAsync();
    }
}