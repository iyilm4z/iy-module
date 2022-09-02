using System;
using Iy.Module.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Iy.Module.Demo.Mvc
{
    [DependsOn(
        typeof(IyKernelModule)
    )]
    public class IyDemoMvcModule : IyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        public override void Configure(IServiceProvider serviceProvider)
        {
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