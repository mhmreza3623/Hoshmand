using FluentValidation;
using Hoshmand.Application.ApplicationServices.Hoshmand;
using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Interfaces.DomainServices;
using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.Shared;
using Hoshmand.Core.Pipeline;
using Hoshmand.Infrastructure.DomainService;
using Hoshmand.Infrastructure.ExternalServices;
using Hoshmand.Infrastructure.Shared;
using Hoshmand.Presentation.Validations;

namespace Hoshmand.Presentation.ServiceCollections
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddTransientConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IHoshmandClientProxy), typeof(HoshmandClientProxy));

            services.AddTransient<IServiceSettings, ServiceSettings>();

            services.AddTransient<IServiceSettings, ServiceSettings>();

            services.AddTransient<IPipeline<HoshmandPipelineContext, HoshmandPipelineRequest, HoshmandPipelineResponse>, HoshmandPipelineProcess>();

            services.AddTransient<IHttpClientUtility, HttpClientUtility>();

            services.AddTransient<IHoshmandClientProxy, HoshmandClientProxy>();

            services.AddScoped<IValidator<AuthenticatonRequestModel>, AuthenticationValidation>();

            return services;
        }
    }
}
