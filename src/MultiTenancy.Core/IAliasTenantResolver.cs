using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public interface IAliasTenantResolver<TTenant>
        where TTenant : ITenant
    {
        Task<TTenant> ResolveAsync(string alias);
    }
}
