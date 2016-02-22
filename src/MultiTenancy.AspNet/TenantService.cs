using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MultiTenancy.AspNet
{
    abstract class TenantService
    {
        internal abstract Task SetTenant(HttpContext context);
        internal abstract object GetTenant();
    }

    class TenantService<TTenant> : TenantService, ITenantService<TTenant>
        where TTenant : class
    {
        private readonly IEnumerable<ITenantProvider<TTenant>> _providers;

        public TTenant Tenant { get; private set; }
        object ITenantService.Tenant => Tenant;

        public TenantService(IEnumerable<ITenantProvider<TTenant>> providers)
        {
            _providers = providers;
        }

        internal override async Task SetTenant(HttpContext context)
        {
            foreach (var provider in _providers)
            {
                var tenant = await provider.GetTenantAsync(context);

                if (tenant != null)
                {
                    Tenant = tenant;
                    return;
                }
            }
        }

        internal override object GetTenant()
        {
            return Tenant;
        }
    }
}
