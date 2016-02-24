using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public class TenantScopeFactory : ITenantScopeFactory
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TenantScopeFactory(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public IServiceScope CreateForTenant<TTenant>(TTenant tenant)
            where TTenant : class, ITenant
        {
            var scope = _serviceScopeFactory.CreateScope();

            var tenantService = scope.ServiceProvider.GetRequiredService<CallContextTenantService<TTenant>>();
            tenantService.Tenant = tenant;

            return scope;
        }
    }
}
