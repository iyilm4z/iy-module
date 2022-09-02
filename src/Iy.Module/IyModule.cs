using System;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Iy.Module
{
    public abstract class IyModule
    {
        public virtual void ConfigureServices(IServiceCollection services)
        {

        }

        public virtual void Configure(IServiceProvider serviceProvider)
        {

        }

        public static bool IsIyModule(Type type)
        {
            var typeInfo = type.GetTypeInfo();
            return
                typeInfo.IsClass &&
                !typeInfo.IsAbstract &&
                !typeInfo.IsGenericType &&
                typeof(IyModule).IsAssignableFrom(type);
        }
    }
}