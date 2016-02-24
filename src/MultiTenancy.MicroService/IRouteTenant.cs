using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy.MicroService
{
    public interface IRouteTenant : ITenant
    {
        string[] Routes { get; }
    }
}
