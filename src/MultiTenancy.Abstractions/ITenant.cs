using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenancy
{
    public interface ITenant
    {
        string Alias { get; }
    }
}
