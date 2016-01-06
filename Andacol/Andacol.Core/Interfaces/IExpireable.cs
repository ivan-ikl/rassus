using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andacol.Core.Interfaces
{
    public interface IExpireable
    {
        DateTime DateStarted { get; set; }

        DateTime DateExpired { get; set; }
    }
}
