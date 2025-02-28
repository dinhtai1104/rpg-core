using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Shared.Events
{
    public interface IEventArgs
    {
        int Id { get; }
    }
}
