using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public interface IAspNetTenantResolver<TTenant>
    {
        Task<TTenant> ResolveAsync(HttpContext context);
    }
}
