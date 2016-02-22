namespace MultiTenancy
{
    public interface ITenantService
    {
        object Tenant { get; }
    }

    public interface ITenantService<TTenant> : ITenantService
    {
        new TTenant Tenant { get; }
    }
}
