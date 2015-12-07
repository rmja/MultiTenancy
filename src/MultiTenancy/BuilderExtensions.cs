using MultiTenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Builder
{
    public static class BuilderExtensions
    {
        public static IApplicationBuilder UseMultiTenant(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MultiTenantMiddleware>();
        }
    }
}
