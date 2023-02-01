namespace Iy.Module.Demo.Console;

public class IyDemoConsoleModule : IyModule
{
    public override void ConfigureServices(IIocRegistrar iocRegistrar)
    {
        iocRegistrar.AddTransient<ConsoleService>();
    }

    public override void Configure(IIocResolver iocResolver)
    {
    }
}
