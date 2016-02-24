using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public interface ITenantScopeFactory
    {
        IServiceScope CreateForTenant<TTenant>(TTenant tenant)
            where TTenant : class, ITenant;
    }
}
