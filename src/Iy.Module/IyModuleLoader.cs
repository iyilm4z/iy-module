using System;
using System.Collections.Generic;
using System.Linq;

namespace Iy.Module
{
    internal static class IyModuleLoader
    {
        public static List<IyModuleInfo> LoadModules(IIocRegistrar iocRegistrar, Type startupModuleType)
        {
            if (iocRegistrar == null)
            {
                throw new ArgumentNullException(nameof(iocRegistrar));
            }

            if (startupModuleType == null)
            {
                throw new ArgumentNullException(nameof(startupModuleType));
            }

            var modules = GetModuleInfos(iocRegistrar, startupModuleType);

            return SortByDependency(modules, startupModuleType);
        }

        private static List<IyModuleInfo> GetModuleInfos(IIocRegistrar iocRegistrar, Type startupModuleType)
        {
            var modules = new List<IyModuleInfo>();

            FillModules(modules, iocRegistrar, startupModuleType);
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

        private static void FillModules(List<IyModuleInfo> modules, IIocRegistrar iocRegistrar, Type startupModuleType)
        {
            var moduleTypes = IyModuleHelper.FindAllModuleTypes(startupModuleType);

            foreach (var moduleType in moduleTypes)
            {
                var module = (IyModule)Activator.CreateInstance(moduleType);

                iocRegistrar.AddSingleton(moduleType, module);

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
            var dependedModuleTypes = IyModuleHelper.FindDependedModuleTypes(module.Type);

            foreach (var dependedModuleType in dependedModuleTypes)
            {
                var dependedModule = modules.FirstOrDefault(x => x.Type == dependedModuleType);

                if (dependedModule == null)
                {
                    throw new Exception("Could not find a depended module "
                                        + dependedModuleType.AssemblyQualifiedName
                                        + " for " + module.Type.AssemblyQualifiedName);
                }

                module.AddDependency(dependedModule);
            }
        }

        private static void EnsureKernelModuleToBeFirst(List<IyModuleInfo> modules)
        {
            var kernelModuleIndex = modules.FindIndex(x => x.Type == typeof(IyKernelModule));
            if (kernelModuleIndex <= 0)
            {
                //It's already the first!
                return;
            }

            var kernelModule = modules[kernelModuleIndex];
            modules.RemoveAt(kernelModuleIndex);
            modules.Insert(0, kernelModule);
        }

        private static void EnsureStartupModuleToBeLast(List<IyModuleInfo> modules, Type startupModuleType)
        {
            var startupModuleIndex = modules.FindIndex(x => x.Type == startupModuleType);
            if (startupModuleIndex >= modules.Count - 1)
            {
                //It's already the last!
                return;
            }

            var startupModule = modules[startupModuleIndex];
            modules.RemoveAt(startupModuleIndex);
            modules.Add(startupModule);
        }
    }
}