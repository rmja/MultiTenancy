using Microsoft.Extensions.DependencyInjection;

namespace MultiTenancy
{
    public class MultiTenancyBuilder<TTenant>
    {
        public IServiceCollection Services { get; private set; }

        public MultiTenancyBuilder(IServiceCollection services)
        {
            Services = services;
        }
    }
}
