using MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MultiTenancy.AspNet.Providers;
using MultiTenancy.AspNet;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static AspNetMultiTenancyBuilder<TTenant> AddAspNet<TTenant>(this MultiTenancyBuilder<TTenant> builder)
            where TTenant : class
        {
            var services = builder.Services;

            services.AddScoped<ITenantService<TTenant>, TenantService<TTenant>>()
                .AddScoped(s => (ITenantService)s.GetService<ITenantService<TTenant>>())
                .AddScoped(s => (TenantService)s.GetService<ITenantService<TTenant>>());

            return new AspNetMultiTenancyBuilder<TTenant>(services);
        }

        public static AspNetMultiTenancyBuilder<TTenant> AddSubDomainProvider<TTenant>(this AspNetMultiTenancyBuilder<TTenant> builder)
            where TTenant : class
        {
            var services = builder.Services;

            services.AddSingleton<ITenantProvider<TTenant>, SubDomainTenantProvider<TTenant>>();

            return builder;
        }
    }
}

namespace MultiTenancy.AspNet
{
    public class AspNetMultiTenancyBuilder<TTenant>
    {
        internal IServiceCollection Services { get; private set; }

        public AspNetMultiTenancyBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
