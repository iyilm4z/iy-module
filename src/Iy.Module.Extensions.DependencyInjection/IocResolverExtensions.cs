using System;

namespace Iy.Module.Extensions.DependencyInjection
{
    public static class IocResolverExtensions
    {
        public static IServiceProvider GetServiceProvider(this IIocResolver iocResolver)
        {
            if (iocResolver is ServiceProviderIocResolver registrar)
            {
                return registrar.ServiceProvider;
            }

            return null;
        }
    }
}