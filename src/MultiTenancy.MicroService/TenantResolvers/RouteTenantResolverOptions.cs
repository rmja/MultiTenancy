using System.Collections.Generic;

namespace MultiTenancy.MicroService.TenantResolvers
{
    public class RouteTenantResolverOptions<TTenant>
        where TTenant : IRouteTenant
    {
        public List<TTenant> Tenants { get; set; }
    }
}
