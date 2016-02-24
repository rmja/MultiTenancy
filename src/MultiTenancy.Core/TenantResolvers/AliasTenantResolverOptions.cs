using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.TenantResolvers
{
    public class AliasTenantResolverOptions<TTenant>
        where TTenant : ITenant
    {
        public List<TTenant> Tenants { get; set; }
    }
}
