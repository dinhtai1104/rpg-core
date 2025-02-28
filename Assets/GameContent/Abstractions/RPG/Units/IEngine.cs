using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.Units
{
    public interface IEngine
    {
        bool Locked { set; get; }
        void Initialize();
        void Execute();
    }
}
