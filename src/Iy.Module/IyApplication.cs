using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module
{
    public class IyApplication
    {
        private readonly IServiceCollection _services;
        private readonly Type _startupModuleType;

        private List<IyModuleInfo> _modules;

        public IyApplication(Type startupModuleType, IServiceCollection services)
        {
            _startupModuleType = startupModuleType ?? throw new ArgumentNullException(nameof(startupModuleType));
            _services = services ?? throw new ArgumentNullException(nameof(services));

            services.AddSingleton(this);
        }

        public void LoadModules()
        {
            _modules = IyModuleLoader.LoadModules(_services, _startupModuleType);

            foreach (var module in _modules)
            {
                module.Instance.ConfigureServices(_services);
            }
        }

        public void InitModules(IServiceProvider serviceProvider)
        {
            foreach (var module in _modules)
            {
                module.Instance.Configure(serviceProvider);
            }
        }
    }
}