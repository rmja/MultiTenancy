using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public class CallContextTenantService<TTenant> : ITenantService, ITenantService<TTenant>
        where TTenant : class, ITenant
    {
        private const string LogicalDataKey = "__Tenant__";

        public TTenant Tenant
        {
            get
            {
                var handle = CallContext.LogicalGetData(LogicalDataKey) as ObjectHandle;
                return handle?.Unwrap() as TTenant;
            }
            set
            {
                CallContext.LogicalSetData(LogicalDataKey, new ObjectHandle(value));
            }
        }

        object ITenantService.Tenant => Tenant;
    }
}
