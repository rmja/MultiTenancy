using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.AspNet.TenantResolvers
{
    public class HostnameTenantResolver<TTenant> : IAspNetTenantResolver<TTenant>
        where TTenant : IHostnameTenant
    {
        private readonly List<TTenant> _tenants;

        public HostnameTenantResolver(IOptions<HostnameTenantResolverOptions<TTenant>> options)
        {
            _tenants = options.Value.Tenants;
        }

        public Task<TTenant> ResolveAsync(HttpContext context)
        {
            var hostname = context.Request.Host.Value.ToLowerInvariant();

            var tenant = _tenants.SingleOrDefault(x => x.Hostnames.Contains(hostname));

            return Task.FromResult(tenant);
        }
    }
}
