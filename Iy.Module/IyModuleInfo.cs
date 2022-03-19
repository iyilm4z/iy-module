using System.Reflection;

namespace Iy.Module
{
    public class IyModuleInfo
    {
        public Assembly Assembly { get; }

        public Type Type { get; }

        public IyModule Instance { get; }

        public List<IyModuleInfo> Dependencies { get; }

        public IyModuleInfo(Type type, IyModule instance)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Instance = instance ?? throw new ArgumentNullException(nameof(instance));

            Assembly = Type.GetTypeInfo().Assembly;
            Dependencies = new List<IyModuleInfo>();
        }

        public void AddDependency(IyModuleInfo moduleInfo)
        {
            if (!Dependencies.Contains(moduleInfo))
            {
                Dependencies.Add(moduleInfo);
            }
        }
    }
}