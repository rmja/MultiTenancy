using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.OptionsModel;
using MicroService;
using Microsoft.Extensions.Logging;

namespace MultiTenancy.MicroService
{
    class MultiTenantMiddleware
    {
        private readonly MicroServiceRequestDelegate _next;
        private readonly IOptions<MultiTenancyOptions> _options;

        public MultiTenantMiddleware(MicroServiceRequestDelegate next, IOptions<MultiTenancyOptions> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(MessageContext context, TenantService service)
        {
            await service.SetTenant(context);

            if (_options.Value.RequireTenant && service.GetTenant() == null)
            {
                context.Ack = AckType.Reject;
            }
            else
            {
                await _next(context);
            }
        }
    }
}
