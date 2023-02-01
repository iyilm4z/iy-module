using Iy.Module.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.AspNetCore
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIyModuleAspNetCore<TStartupModule>(this IServiceCollection services)
            where TStartupModule : IyModule
        {
            services.AddApplicationBuilderAccessor();

            services.AddIyModule<TStartupModule>();
        }

        public static void AddApplicationBuilderAccessor(this IServiceCollection services)
        {
            services.AddSingleton(new ObjectAccessor<IApplicationBuilder>());
        }
    }
}