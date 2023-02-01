using System;

namespace Iy.Module.Extensions.DependencyInjection
{
    public static class ServiceProviderExtensions
    {
        public static void UseIyModule(this IServiceProvider serviceProvider)
        {
            var iocResolver = new ServiceProviderIocResolver(serviceProvider);
            iocResolver.UseIyModule();
        }
    }
}