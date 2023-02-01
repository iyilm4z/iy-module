using System;

namespace Iy.Module
{
    public interface IIocRegistrar
    {
        void AddSingleton<TService>(TService implementationInstance) where TService : class;

        void AddSingleton(Type serviceType, object implementationInstance);

        void AddTransient<TService>() where TService : class;
    }
}