using System;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module
{
    public static class IyModuleServiceProviderExtensions
    {
        public static IServiceProvider UseIyModule(this IServiceProvider serviceProvider)
        {
            var iyApp = serviceProvider.GetRequiredService<IyApplication>();
            iyApp.InitModules(serviceProvider);

            return serviceProvider;
        }
    }
}
