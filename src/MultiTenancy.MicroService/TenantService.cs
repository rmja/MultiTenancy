using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MicroService;

namespace MultiTenancy.MicroService
{
    abstract class TenantService
    {
        internal abstract Task SetTenant(MessageContext context);
        internal abstract ITenant GetTenant();
    }

    class TenantService<TTenant> : TenantService, ITenantService<TTenant>
        where TTenant : ITenant
    {
        private readonly IMicroServiceTenantResolver<TTenant> _resolver;

        public TTenant Tenant { get; private set; }
        ITenant ITenantService.Tenant => Tenant;

        public TenantService(IMicroServiceTenantResolver<TTenant> resolver)
        {
            _resolver = resolver;
        }

        internal override async Task SetTenant(MessageContext context)
        {
            var tenant = await _resolver.ResolveAsync(context);

            if (tenant != null)
            {
                Tenant = tenant;
                return;
            }
        }

        internal override ITenant GetTenant()
        {
            return Tenant;
        }
    }
}
