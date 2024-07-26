using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.Shared.Pool
{
    public interface IPool<T>
    {
        T Allocate();
        bool Recycle(T obj);
    }
}
