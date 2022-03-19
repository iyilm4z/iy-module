using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Iy.Module
{
    internal class ModuleLoader
    {
        public static List<IyModuleInfo> LoadModules(IServiceCollection services, Type startupModuleType)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (startupModuleType == null)
            {
                throw new ArgumentNullException(nameof(startupModuleType));
            }

            var modules = GetModuleInfos(services, startupModuleType);

            modules = SortByDependency(modules, startupModuleType);

            return modules;
        }

        private static List<IyModuleInfo> GetModuleInfos(IServiceCollection services, Type startupModuleType)
        {
            var modules = new List<IyModuleInfo>();

            FillModules(modules, services, startupModuleType);
            SetDependencies(modules);

            return modules;
        }

        private static List<IyModuleInfo> SortByDependency(List<IyModuleInfo> modules, Type startupModuleType)
        {
            var sortedModules = modules.SortByDependencies(x => x.Dependencies);

            EnsureKernelModuleToBeFirst(sortedModules);
            EnsureStartupModuleToBeLast(sortedModules, startupModuleType);

            return sortedModules;
        }

        private static void FillModules(List<IyModuleInfo> modules, IServiceCollection services, Type startupModuleType)
        {
            var moduleTypes = FindAllModuleTypes(startupModuleType);

            foreach (var moduleType in moduleTypes)
            {
                var module = (IyModule)Activator.CreateInstance(moduleType);

                services.AddSingleton(moduleType, module);

                var moduleInfo = new IyModuleInfo(moduleType, module);

                modules.Add(moduleInfo);
            }
        }

        private static void SetDependencies(List<IyModuleInfo> modules)
        {
            foreach (var module in modules)
            {
                SetDependencies(modules, module);
            }
        }

        private static void SetDependencies(List<IyModuleInfo> modules, IyModuleInfo module)
        {
            var dependedModuleTypes = FindDependedModuleTypes(module.Type);

            foreach (var dependedModuleType in dependedModuleTypes)
            {
                var dependedModule = modules.FirstOrDefault(m => m.Type == dependedModuleType);

                if (dependedModule == null)
                {
                    throw new Exception("Could not find a depended module "
                        + dependedModuleType.AssemblyQualifiedName
                        + " for " + module.Type.AssemblyQualifiedName);
                }

                module.AddDependency(dependedModule);
            }
        }

        public static void EnsureKernelModuleToBeFirst(List<IyModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(m => m.Type == typeof(IyKernelModule));
            if (kernelModuleIndex <= 0)
            {
                //It's already the first!
                return;
            }

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        public static void EnsureStartupModuleToBeLast(List<IyModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(m => m.Type == startupModuleType);
            if (startupModuleIndex >= modules.Count - 1)
            {
                //It's already the last!
                return;
            }

            var startupModule = modules[startupModuleIndex];
            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }

        public static List<Type> FindAllModuleTypes(Type startupModuleType)
        {
            var moduleTypes = new List<Type>();

            AddModuleAndDependenciesRecursively(moduleTypes, startupModuleType);

            if (!moduleTypes.Contains(typeof(IyKernelModule)))
            {
                moduleTypes.Add(typeof(IyKernelModule));
            }

            return moduleTypes;
        }

        public static List<Type> FindDependedModuleTypes(Type moduleType)
        {
            if (!IyModule.IsIyModule(moduleType))
            {
                throw new ArgumentNullException(nameof(moduleType));
            }

            var dependencies = new List<Type>();

            if (moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                var dependsOnAttributes = moduleType.GetTypeInfo()
                    .GetCustomAttributes(typeof(DependsOnAttribute), true)
                    .Cast<DependsOnAttribute>();

                foreach (var dependsOnAttribute in dependsOnAttributes)
                {
                    foreach (var dependedModuleType in dependsOnAttribute.DependedModuleTypes)
                    {
                        dependencies.Add(dependedModuleType);
                    }
                }
            }

            return dependencies;
        }

        private static void AddModuleAndDependenciesRecursively(List<Type> moduleTypes, Type moduleType)
        {
            if (!IyModule.IsIyModule(moduleType))
            {
                throw new ArgumentNullException(nameof(moduleType));
            }

            if (moduleTypes.Contains(moduleType))
            {
                return;
            }

            moduleTypes.Add(moduleType);

            var innerModuleTypes = FindDependedModuleTypes(moduleType);

            foreach (var dependedModuleType in innerModuleTypes)
            {
                AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType);
            }
        }
    }
}
