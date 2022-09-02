using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Iy.Module
{
    internal static class IyModuleHelper
    {
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
                    dependencies.AddRange(dependsOnAttribute.DependedModuleTypes);
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