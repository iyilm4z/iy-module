using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.AspNetCore
{
    public static class IyModuleAspNetCoreApplicationBuilderExtensions
    {
        public static void UseIyModuleAspNetCore(this IApplicationBuilder app)
        {
            app.UseApplicationBuilderAccessor();

            app.ApplicationServices.UseIyModule();
        }

        public static void UseApplicationBuilderAccessor(this IApplicationBuilder app)
        {
            app.ApplicationServices.GetRequiredService<ObjectAccessor<IApplicationBuilder>>().Value = app;
        }
    }
}