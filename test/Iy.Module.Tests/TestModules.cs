using System;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module.Tests
{
    [DependsOn]
    public class TestModule1 : IyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IServiceProvider serviceProvider)
        {
        }
    }

    [DependsOn(typeof(TestModule1))]
    public class TestModule2 : IyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IServiceProvider serviceProvider)
        {
        }
    }

    [DependsOn(typeof(TestModule1), typeof(TestModule2))]
    public class TestModule3 : IyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IServiceProvider serviceProvider)
        {
        }
    }

    [DependsOn(typeof(TestModule3))]
    public class TestModule4 : IyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IServiceProvider serviceProvider)
        {
        }
    }

    [DependsOn(typeof(TestModule4))]
    public class TestModule5 : IyModule
    {
        public override void ConfigureServices(IServiceCollection services)
        {
        }

        public override void Configure(IServiceProvider serviceProvider)
        {
        }
    }
}