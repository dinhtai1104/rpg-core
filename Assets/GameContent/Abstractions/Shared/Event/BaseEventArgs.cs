using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Shared.Events
{
    public class BaseEventArgs<T> : IEventArgs where T : IEventArgs
    {
        public int Id
        {
            get { return typeof(T).GetHashCode(); }
        }
    }
}
