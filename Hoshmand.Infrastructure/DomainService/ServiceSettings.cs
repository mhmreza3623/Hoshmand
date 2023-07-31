using Hoshmand.Core.Interfaces.DomainServices;
using Microsoft.Extensions.Configuration;

namespace Hoshmand.Infrastructure.DomainService
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
