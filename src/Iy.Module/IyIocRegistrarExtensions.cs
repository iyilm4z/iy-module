namespace Iy.Module
{
    public static class IyIocRegistrarExtensions
    {
        public static IIocRegistrar AddIyModule<TStartupModule>(this IIocRegistrar iocRegistrar)
            where TStartupModule : IyModule
        {
            var iyApp = new IyApplication(typeof(TStartupModule), iocRegistrar);
            iyApp.LoadModules();

            iocRegistrar.AddSingleton(iocRegistrar);
            iocRegistrar.AddSingleton(new ObjectAccessor<IIocResolver>());

            return iocRegistrar;
        }
    }
}