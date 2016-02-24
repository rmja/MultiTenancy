using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using MultiTenancy;
using MultiTenancy.AspNet;
using MultiTenancy.AspNet.TenantResolvers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static MultiTenancyBuilder<TTenant> AddAspNetHostnameTenancy<TTenant>(this MultiTenancyBuilder<TTenant> builder, IConfiguration configuration)
            where TTenant : IHostnameTenant
        {
            var services = builder.Services;

            services.AddScoped<ITenantService<TTenant>, TenantService<TTenant>>()
                .AddScoped(s => (ITenantService)s.GetService<ITenantService<TTenant>>())
                .AddScoped(s => (TenantService)s.GetService<ITenantService<TTenant>>());

            services.AddSingleton<IAspNetTenantResolver<TTenant>, HostnameTenantResolver<TTenant>>();

            services.Configure<HostnameTenantResolverOptions<TTenant>>(configuration);

            return builder;
        }
    }
}