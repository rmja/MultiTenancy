using System.Collections.Generic;

namespace MultiTenancy.AspNet.TenantResolvers
{
    public class HostnameTenantResolverOptions<TTenant>
        where TTenant : IHostnameTenant
    {
        public List<TTenant> Tenants { get; set; }
    }
}
