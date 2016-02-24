using Microsoft.Extensions.OptionsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.TenantResolvers
{
    public class AliasTenantResolver<TTenant> : IAliasTenantResolver<TTenant>
        where TTenant : ITenant
    {
        private readonly List<TTenant> _tenants;

        public AliasTenantResolver(IOptions<AliasTenantResolverOptions<TTenant>> options)
        {
            _tenants = options.Value.Tenants;
        }

        public Task<TTenant> ResolveAsync(string alias)
        {
            return Task.FromResult(_tenants.SingleOrDefault(x => x.Alias == alias));
        }
    }
}
