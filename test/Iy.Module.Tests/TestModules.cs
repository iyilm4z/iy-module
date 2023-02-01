namespace Iy.Module.Tests
{
    public class TestModule1 : IyModule
    {
        public override void ConfigureServices(IIocRegistrar iocRegistrar)
        {
        }

        public override void Configure(IIocResolver iocResolver)
        {
        }
    }

    [DependsOn(typeof(TestModule1))]
    public class TestModule2 : IyModule
    {
        public override void ConfigureServices(IIocRegistrar iocRegistrar)
        {
        }

        public override void Configure(IIocResolver iocResolver)
        {
        }
    }

    [DependsOn(typeof(TestModule1), typeof(TestModule2))]
    public class TestModule3 : IyModule
    {
        public override void ConfigureServices(IIocRegistrar iocRegistrar)
        {
        }

        public override void Configure(IIocResolver iocResolver)
        {
        }
    }

    [DependsOn(typeof(TestModule3))]
    public class TestModule4 : IyModule
    {
        public override void ConfigureServices(IIocRegistrar iocRegistrar)
        {
        }

        public override void Configure(IIocResolver iocResolver)
        {
        }
    }

    [DependsOn(typeof(TestModule4))]
    public class TestModule5 : IyModule
    {
        public override void ConfigureServices(IIocRegistrar iocRegistrar)
        {
        }

        public override void Configure(IIocResolver iocResolver)
        {
        }
    }
}