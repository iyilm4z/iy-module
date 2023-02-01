using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.Extensions.DependencyInjection
{
    public static class IocRegistrarExtensions
    {
        public static IServiceCollection GetServiceCollection(this IIocRegistrar iocRegistrar)
        {
            if (iocRegistrar is ServiceCollectionIocRegistrar registrar)
            {
                return registrar.ServiceCollection;
            }

            return null;
        }
    }
}