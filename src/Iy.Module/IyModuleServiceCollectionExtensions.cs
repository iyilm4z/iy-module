using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module
{
    public static class IyModuleServiceCollectionExtensions
    {
        public static IServiceCollection AddIyModule<TStartupModule>(this IServiceCollection services)
            where TStartupModule : IyModule
        {
            var iyApp = new IyApplication(typeof(TStartupModule), services);
            iyApp.LoadModules();

            return services;
        }
    }
}
