using MicroService;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public interface ITenantProvider<TTenant>
        where TTenant : class
    {
        Task<TTenant> GetTenantAsync(MessageContext context);
    }
}
