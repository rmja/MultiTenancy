﻿using Microsoft.AspNet.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace MultiTenancy.AspNet
{
    public abstract class TenantService
    {
        internal abstract Task SetTenant(HttpContext context);
        internal abstract ITenant GetTenant();
    }

    public class TenantService<TTenant> : TenantService, ITenantService<TTenant>
        where TTenant : ITenant
    {
        private readonly IAspNetTenantResolver<TTenant> _resolver;

        public TTenant Tenant { get; private set; }
        ITenant ITenantService.Tenant => Tenant;

        public TenantService(IAspNetTenantResolver<TTenant> resolver)
        {
            _resolver = resolver;
        }

        internal override async Task SetTenant(HttpContext context)
        {
            Tenant = await _resolver.ResolveAsync(context);
        }

        internal override ITenant GetTenant()
        {
            return Tenant;
        }
    }
}
