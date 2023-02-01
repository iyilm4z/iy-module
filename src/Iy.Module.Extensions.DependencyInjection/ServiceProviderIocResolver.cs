using System;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.Extensions.DependencyInjection
{
    public class ServiceProviderIocResolver : IIocResolver
    {
        public IServiceProvider ServiceProvider { get; }

        public ServiceProviderIocResolver(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public T ResolveRequired<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }
    }
}