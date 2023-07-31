﻿using FluentValidation;
using Hoshmand.Application.PipeLines.Hoshmand;
using Hoshmand.Core.Dto.Requests;
using Hoshmand.Core.Interfaces.ApplicationServices;
using Hoshmand.Core.Interfaces.DomainServices;
using Hoshmand.Core.Interfaces.ExternalServices;
using Hoshmand.Core.Interfaces.Shared;
using Hoshmand.Infrastructure.DomainService;
using Hoshmand.Infrastructure.ExternalServices;
using Hoshmand.Infrastructure.Repositories;
using Hoshmand.Infrastructure.Shared;
using Hoshmand.Presentation.Validations;
using System;

namespace Hoshmand.Presentation.ServiceCollections
{
    public static class ServicesConfig
    {
        public static IServiceCollection AddTransientConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient(typeof(IHoshmandClientProxy), typeof(HoshmandClientProxy));

            services.AddTransient<IServiceSettings, ServiceSettings>();

            services.AddTransient<IServiceSettings, ServiceSettings>();

            services.AddTransient<IHoshmandAppService, HoshmandAppService>();

            services.AddTransient<IHttpClientUtility, HttpClientUtility>();

            services.AddTransient<IHoshmandClientProxy, HoshmandClientProxy>();

            services.AddScoped<IValidator<AuthenticatonRequestModel>, AuthenticationValidation>();


            return services;
        }
    }
}
