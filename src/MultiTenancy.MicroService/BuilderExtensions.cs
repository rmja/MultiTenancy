using MultiTenancy.MicroService;

namespace MicroService
{
    public static class BuilderExtensions
    {
        public static IMicroServiceBuilder UseMultiTenant(this IMicroServiceBuilder app)
        {
            return app.UseMiddleware<MultiTenantMiddleware>();
        }
    }
}
