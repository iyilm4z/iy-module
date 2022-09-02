using Iy.Module.AspNetCore;
using Iy.Module.Demo.Mvc;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIyModuleAspNetCore<IyDemoMvcModule>();

var app = builder.Build();

app.UseIyModuleAspNetCore();

app.Run();