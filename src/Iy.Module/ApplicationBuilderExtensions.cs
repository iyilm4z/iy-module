using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIyModule(this IApplicationBuilder app)
        {
            var iyApp = app.ApplicationServices.GetRequiredService<IyApplication>();
            iyApp.InitModules(app);

            return app;
        }
    }
}
