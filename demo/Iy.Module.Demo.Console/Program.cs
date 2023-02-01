using Iy.Module.Demo.Console;
using Iy.Module.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddIyModule<IyDemoConsoleModule>();

var serviceProvider = services.BuildServiceProvider();
serviceProvider.UseIyModule();

var consoleService = serviceProvider.GetRequiredService<ConsoleService>();
consoleService.EchoHelloWorld();