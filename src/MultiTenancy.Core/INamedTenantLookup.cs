using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public interface INamedTenantLookup<TTenant>
    {
        Task<TTenant> LookupAsync(string name);
    }
}
