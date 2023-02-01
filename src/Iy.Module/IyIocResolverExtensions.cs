namespace Iy.Module
{
    public static class IyIocResolverExtensions
    {
        public static IIocResolver UseIyModule(this IIocResolver iocResolver)
        {
            var iyApp = iocResolver.ResolveRequired<IyApplication>();
            iyApp.InitModules(iocResolver);

            var iocResolverObjectAccessor = iocResolver.ResolveRequired<ObjectAccessor<IIocResolver>>();
            iocResolverObjectAccessor.Value = iocResolver;

            return iocResolver;
        }
    }
}