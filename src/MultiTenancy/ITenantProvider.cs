using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public interface ITenantProvider<TTenant>
        where TTenant : class
    {
        Task<TTenant> GetTenantAsync(HttpContext context);
    }
}
