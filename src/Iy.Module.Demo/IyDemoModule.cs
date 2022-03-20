namespace Iy.Module.Demo
{
    [DependsOn(
        typeof(IyKernelModule)
    )]
    public class IyDemoModule : IyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
        }

        public override void Configure(IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

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