using MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MultiTenancy.Providers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static MultiTenancyBuilder<TTenant> AddMultiTenancy<TTenant>(this IServiceCollection services, Action<MultiTenancyOptions> setupAction = null)
            where TTenant : class
        {
            services.AddOptions();

            services.AddScoped<ITenantService<TTenant>, TenantService<TTenant>>()
                .AddScoped(s => (ITenantService)s.GetService<ITenantService<TTenant>>())
                .AddScoped(s => (TenantService)s.GetService<ITenantService<TTenant>>());

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            return new MultiTenancyBuilder<TTenant>(services);
        }

        public static MultiTenancyBuilder<TTenant> AddSubDomainProvider<TTenant>(this MultiTenancyBuilder<TTenant> builder)
            where TTenant : class
        {
            var services = builder.Services;

            services.AddSingleton<ITenantProvider<TTenant>, SubDomainTenantProvider<TTenant>>();

            return builder;
        }
    }
}

namespace MultiTenancy
{
    public class MultiTenancyBuilder<TTenant>
    {
        internal IServiceCollection Services { get; private set; }

        public MultiTenancyBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
