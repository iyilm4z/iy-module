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

            var dependedModuleTypes = new List<Type>();

            if (!moduleType.GetTypeInfo().IsDefined(typeof(DependsOnAttribute), true))
            {
                return dependedModuleTypes;
            }

            var dependsOnAttributes = moduleType.GetTypeInfo()
                .GetCustomAttributes(typeof(DependsOnAttribute), true)
                .Cast<DependsOnAttribute>();

            foreach (var dependsOnAttribute in dependsOnAttributes)
            {
                dependedModuleTypes.AddRange(dependsOnAttribute.DependedModuleTypes);
            }

            return dependedModuleTypes;
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

            var dependedModuleTypes = FindDependedModuleTypes(moduleType);

            foreach (var dependedModuleType in dependedModuleTypes)
            {
                AddModuleAndDependenciesRecursively(moduleTypes, dependedModuleType);
            }
        }
    }
}