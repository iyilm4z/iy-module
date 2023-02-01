using Iy.Module.AspNetCore;
using Iy.Module.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Iy.Module.Demo.Mvc
{
    public class IyDemoMvcModule : IyModule
    {
        public override void ConfigureServices(IIocRegistrar iocRegistrar)
        {
            iocRegistrar.GetServiceCollection().AddRazorPages();
        }

        public override void Configure(IIocResolver iocResolver)
        {
            var serviceProvider = iocResolver.GetServiceProvider();
            var env = serviceProvider.GetRequiredWebHostEnvironment();
            var app = serviceProvider.GetRequiredApplicationBuilder();

            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}
