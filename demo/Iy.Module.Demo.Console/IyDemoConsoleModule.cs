using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.Demo.Console;

public class IyDemoConsoleModule : IyModule
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ConsoleService>();
    }

    public override void Configure(IServiceProvider serviceProvider)
    {
    }
}
