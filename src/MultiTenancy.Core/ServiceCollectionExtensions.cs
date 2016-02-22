using MultiTenancy;
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
    }
}