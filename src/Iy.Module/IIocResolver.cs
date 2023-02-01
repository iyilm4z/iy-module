namespace Iy.Module
{
    public interface IIocResolver
    {
        T ResolveRequired<T>();
    }
}