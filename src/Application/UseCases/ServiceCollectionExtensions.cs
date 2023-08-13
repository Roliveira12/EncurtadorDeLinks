using Application.UseCases.CreateShortenerUrl;
using Microsoft.Extensions.DependencyInjection;
using GetUseCase = Application.UseCases.GetShortenerUrlUseCase;

namespace Application.UseCases
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {

            //UseCases
            services.AddScoped<GetUseCase.IGetShortenerUrlUseCase, GetUseCase.GetShortenerUrlUseCase>();

            services.AddScoped<ICreateShortenerUrlUseCase, CreateShortenerUrlUseCase>();

            return services;
        }
    }
}