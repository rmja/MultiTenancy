using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.OptionsModel;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenancy.AspNet
{
    class MultiTenantMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IOptions<MultiTenancyOptions> _options;

        public MultiTenantMiddleware(RequestDelegate next, IOptions<MultiTenancyOptions> options)
        {
            _next = next;
            _options = options;
        }

        public async Task Invoke(HttpContext context, TenantService tenantService)
        {
            await tenantService.SetTenant(context);

            if (_options.Value.RequireTenant && tenantService.GetTenant() == null)
            {
                context.Response.StatusCode = 404;
            }
            else
            {
                await _next(context);
            }
        }
    }
}
