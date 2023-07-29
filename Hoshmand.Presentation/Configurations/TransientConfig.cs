using Hoshmand.Application.ApplicationServices;
using Hoshmand.Core.Interfaces.ApplicationServices;
using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.SettingServices;
using Hoshmand.Infrastructure.AppSettings;
using Hoshmand.Infrastructure.ExternalServices;
using Hoshmand.Infrastructure.Repositories;

namespace Hoshmand.Presentation.ServiceCollections
{
    public static class TransientConfig
    {
        public static IServiceCollection AddTransientConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IHoshmandClientProxy), typeof(HoshmandClientProxy));

            services.AddTransient<IServiceSettings, ServiceSettings>();

            services.AddTransient<IServiceSettings, ServiceSettings>();

            services.AddTransient<IHoshmandAppService, HoshmandAppService>();

            return services;
        }
    }
}
