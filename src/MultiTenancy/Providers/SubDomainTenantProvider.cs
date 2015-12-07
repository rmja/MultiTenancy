using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenancy.Providers
{
    public class SubDomainTenantProvider<TTenant> : ITenantProvider<TTenant>
        where TTenant : class
    {
        private readonly static Regex _portNumberRegex = new Regex(":\\d{1,5}$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private readonly ILogger _logger;

        public SubDomainTenantProvider(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SubDomainTenantProvider<TTenant>>();
        }

        public async Task<TTenant> GetTenantAsync(HttpContext context)
        {
            if (context.Request.Host != null)
            {
                var hostname = context.Request.Host.Value;

                // Remove port number
                hostname = _portNumberRegex.Replace(hostname, "");

                var subDomain = GetSubDomain(hostname);

                if (!string.IsNullOrEmpty(subDomain))
                {
                    _logger.LogDebug($"Sub domain is {subDomain}.");

                    var tenantProvider = context.RequestServices.GetRequiredService<INamedTenantLookup<TTenant>>();
                    var tenant = await tenantProvider.LookupAsync(subDomain);
                    return tenant;
                }
                else
                {
                    _logger.LogInformation($"No sub-domain in {hostname}.");
                }
            }

            return null;
        }

        private string GetSubDomain(string hostname)
        {
            var parts = hostname.Split('.');

            if (parts.Last() == "localhost")
            {
                return string.Join(".", parts.Take(parts.Length - 1));
            }
            else
            {
                return string.Join(".", parts.Take(parts.Length - 2));
            }
        }
    }
}
