using System;
using System.Collections.Generic;

namespace Iy.Module
{
    public class IyApplication
    {
        private readonly IIocRegistrar _iocRegistrar;
        private readonly Type _startupModuleType;

        private List<IyModuleInfo> _modules;

        public IyApplication(Type startupModuleType, IIocRegistrar iocRegistrar)
        {
            _startupModuleType = startupModuleType ?? throw new ArgumentNullException(nameof(startupModuleType));
            _iocRegistrar = iocRegistrar ?? throw new ArgumentNullException(nameof(iocRegistrar));

            iocRegistrar.AddSingleton(this);
        }

        public void LoadModules()
        {
            _modules = IyModuleLoader.LoadModules(_iocRegistrar, _startupModuleType);

            foreach (var module in _modules)
            {
                module.Instance.ConfigureServices(_iocRegistrar);
            }
        }

        public void InitModules(IIocResolver iocResolver)
        {
            foreach (var module in _modules)
            {
                module.Instance.Configure(iocResolver);
            }
        }
    }
}