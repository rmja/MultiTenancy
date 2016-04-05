namespace MultiTenancy
{
    public interface ITenantService
    {
        ITenant Tenant { get; }
    }

    public interface ITenantService<TTenant> : ITenantService
        where TTenant : ITenant
    {
        new TTenant Tenant { get; }
    }
}
