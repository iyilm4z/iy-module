using Iy.Module;
using Iy.Module.Demo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIyModule<IyDemoModule>();

var app = builder.Build();

app.UseIyModule();

app.Run();