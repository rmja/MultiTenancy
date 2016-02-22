using MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MultiTenancy.MicroService;
using MultiTenancy.MicroService.Providers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static MicroServiceMultiTenancyBuilder<TTenant> AddMicroService<TTenant>(this MultiTenancyBuilder<TTenant> builder)
            where TTenant : class
        {
            var services = builder.Services;

            services.AddScoped<ITenantService<TTenant>, TenantService<TTenant>>()
                .AddScoped(s => (ITenantService)s.GetService<ITenantService<TTenant>>())
                .AddScoped(s => (TenantService)s.GetService<ITenantService<TTenant>>());

            return new MicroServiceMultiTenancyBuilder<TTenant>(services);
        }

        public static MicroServiceMultiTenancyBuilder<TTenant> AddFirstRouteWordProvider<TTenant>(this MicroServiceMultiTenancyBuilder<TTenant> builder)
            where TTenant : class
        {
            var services = builder.Services;

            services.AddSingleton<ITenantProvider<TTenant>, FirstRouteWordTenantProvider<TTenant>>();

            return builder;
        }
    }
}

namespace MultiTenancy.MicroService
{
    public class MicroServiceMultiTenancyBuilder<TTenant>
    {
        internal IServiceCollection Services { get; private set; }

        public MicroServiceMultiTenancyBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
