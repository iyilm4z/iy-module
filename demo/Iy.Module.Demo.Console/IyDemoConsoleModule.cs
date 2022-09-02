using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.Demo.Console;

[DependsOn(
    typeof(IyKernelModule)
)]
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