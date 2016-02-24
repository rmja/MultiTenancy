using Microsoft.Extensions.Configuration;
using MultiTenancy;
using MultiTenancy.TenantResolvers;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static MultiTenancyBuilder<TTenant> AddMultiTenancy<TTenant>(this IServiceCollection services, Action<MultiTenancyOptions> setupAction = null)
            where TTenant : class
        {
            services.AddOptions();

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            return new MultiTenancyBuilder<TTenant>(services);
        }

        public static MultiTenancyBuilder<TTenant> AddAliasTenancy<TTenant>(this MultiTenancyBuilder<TTenant> builder, IConfiguration configuration)
            where TTenant : class, ITenant
        {
            var services = builder.Services;

            services.AddScoped<ITenantService<TTenant>, CallContextTenantService<TTenant>>()
                .AddScoped(s => (ITenantService)s.GetService<ITenantService<TTenant>>())
                .AddScoped(s => (CallContextTenantService<TTenant>)s.GetService<ITenantService<TTenant>>());

            services.AddSingleton<IAliasTenantResolver<TTenant>, AliasTenantResolver<TTenant>>();
            services.AddSingleton<ITenantScopeFactory, TenantScopeFactory>();

            services.Configure<AliasTenantResolverOptions<TTenant>>(configuration);

            return builder;
        }
    }
}