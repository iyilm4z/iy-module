using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.AspNetCore
{
    public static class IyModuleAspNetCoreServiceProviderExtensions
    {
        public static IApplicationBuilder GetRequiredApplicationBuilder(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value;
        }

        public static IWebHostEnvironment GetRequiredWebHostEnvironment(this IServiceProvider serviceProvider)
        {
            return serviceProvider.GetRequiredService<IWebHostEnvironment>();
        }
    }
}
