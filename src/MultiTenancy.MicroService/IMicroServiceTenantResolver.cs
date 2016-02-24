using MicroService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.MicroService
{
    public interface IMicroServiceTenantResolver<TTenant>
    {
        Task<TTenant> ResolveAsync(MessageContext context);
    }
}
