using System;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.Extensions.DependencyInjection
{
    public class ServiceCollectionIocRegistrar : IIocRegistrar
    {
        public IServiceCollection ServiceCollection { get; }

        public ServiceCollectionIocRegistrar(IServiceCollection serviceCollection)
        {
            ServiceCollection = serviceCollection;
        }

        public void AddSingleton<TService>(TService implementationInstance) where TService : class
        {
            ServiceCollection.AddSingleton(implementationInstance);
        }

        public void AddSingleton(Type serviceType, object implementationInstance)
        {
            ServiceCollection.AddSingleton(serviceType, implementationInstance);
        }

        public void AddTransient<TService>() where TService : class
        {
            ServiceCollection.AddTransient<TService>();
        }
    }
}