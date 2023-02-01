using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIyModule<TStartupModule>(this IServiceCollection services)
            where TStartupModule : IyModule
        {
            var iocRegistrar = new ServiceCollectionIocRegistrar(services);
            iocRegistrar.AddIyModule<TStartupModule>();
        }
    }
}