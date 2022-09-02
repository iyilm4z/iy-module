using Iy.Module;
using Iy.Module.Demo.Console;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

services.AddIyModule<IyDemoConsoleModule>();

var serviceProvider = services.BuildServiceProvider();

serviceProvider.UseIyModule();

var consoleService = serviceProvider.GetRequiredService<ConsoleService>();
consoleService.EchoHelloWorld();