using Microsoft.Extensions.DependencyInjection;

namespace AniRateV1.ViewModels
{
    static class ViewModelsRegistrator
    {
        public static IServiceCollection AddViewModels(this IServiceCollection service) => service
            .AddSingleton<MainWindowViewModel>()
            .AddSingleton<ExactAnimeTitleViewModel>()
            .AddSingleton<CollectionViewModel>()
            ;
    }
}
