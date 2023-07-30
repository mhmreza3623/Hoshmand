using Hoshmand.Core.Interfaces.SettingServices;
using Microsoft.Extensions.Configuration;

namespace Hoshmand.Infrastructure.AppSettings
{
    public class ServiceSettings : IServiceSettings
    {
        public ServiceSettings(IConfiguration configuration)
        {
            HoshmandOrderBaseAddress = configuration.GetSection("ServiceApiAddress")["HoshmandOrderBaseAddress"];
            HoshmandIdCardBaseAddress = configuration.GetSection("ServiceApiAddress")["HoshmandIdCardBaseAddress"];
        }

        public string HoshmandOrderBaseAddress { get; }
        public string HoshmandIdCardBaseAddress { get; set; }

    }
}
