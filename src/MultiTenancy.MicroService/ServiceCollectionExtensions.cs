using Microsoft.Extensions.Configuration;
using MultiTenancy;
using MultiTenancy.MicroService;
using MultiTenancy.MicroService.TenantResolvers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static MultiTenancyBuilder<TTenant> AddMicroServiceRouteTenancy<TTenant>(this MultiTenancyBuilder<TTenant> builder, IConfiguration configuration)
            where TTenant : IRouteTenant
        {
            var services = builder.Services;

            services.AddScoped<ITenantService<TTenant>, TenantService<TTenant>>()
                .AddScoped(s => (ITenantService)s.GetService<ITenantService<TTenant>>())
                .AddScoped(s => (TenantService)s.GetService<ITenantService<TTenant>>());

            services.AddSingleton<IMicroServiceTenantResolver<TTenant>, RouteTenantResolver<TTenant>>();

            services.Configure<RouteTenantResolverOptions<TTenant>>(configuration);

            return builder;
        }
    }
}