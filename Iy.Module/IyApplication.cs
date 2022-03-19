using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Iy.Module
{
    public class IyApplication
    {
        private readonly IServiceCollection _services;
        private readonly Type _startupType;

        private List<IyModuleInfo> _modules;

        public IyApplication(Type startupModule, IServiceCollection services)
        {
            _startupType = startupModule ?? throw new ArgumentNullException(nameof(startupModule));
            _services = services ?? throw new ArgumentNullException(nameof(services));

            services.AddSingleton(this);
        }

        public void LoadModules()
        {
            _modules = ModuleLoader.LoadModules(_services, _startupType);

            foreach (var module in _modules)
            {
                module.Instance.ConfigureServices(_services);
            }
        }

        public void InitModules(IApplicationBuilder app)
        {
            foreach (var module in _modules)
            {
                module.Instance.Configure(app);
            }
        }
    }
}