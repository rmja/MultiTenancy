using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroService;
using Microsoft.Extensions.OptionsModel;

namespace MultiTenancy.MicroService.TenantResolvers
{
    public class RouteTenantResolver<TTenant> : IMicroServiceTenantResolver<TTenant>
        where TTenant : IRouteTenant
    {
        private readonly List<TTenant> _tenants;

        public RouteTenantResolver(IOptions<RouteTenantResolverOptions<TTenant>> options)
        {
            _tenants = options.Value.Tenants;
        }

        public Task<TTenant> ResolveAsync(MessageContext context)
        {
            var route = context.Route;

            var tenant = _tenants.SingleOrDefault(x => x.Routes.Any(pattern => RouteMatching.RouteMatchesPattern(route, pattern)));

            return Task.FromResult(tenant);
        }
    }
}
