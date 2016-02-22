using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenancy.MicroService.Providers
{
    public class FirstRouteWordTenantProvider<TTenant> : ITenantProvider<TTenant>
        where TTenant : class
    {
        private readonly ILogger _logger;

        public FirstRouteWordTenantProvider(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FirstRouteWordTenantProvider<TTenant>>();
        }

        public async Task<TTenant> GetTenantAsync(MessageContext context)
        {
            var first = context.Route.Split('.').First();

            _logger.LogDebug($"First word in route is {first}.");

            var tenantLookup = context.RequestServices.GetRequiredService<INamedTenantLookup<TTenant>>();
            var tenant = await tenantLookup.LookupAsync(first);
            return tenant;
        }
    }
}
