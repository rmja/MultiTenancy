using MultiTenancy.AspNet;

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
