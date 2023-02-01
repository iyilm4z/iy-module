using System;
using System.Reflection;

namespace Iy.Module
{
    public abstract class IyModule
    {
        public virtual void ConfigureServices(IIocRegistrar iocRegistrar)
        {
        }

        public virtual void Configure(IIocResolver iocResolver)
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