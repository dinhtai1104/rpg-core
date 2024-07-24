using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.Units.Engine.Healths
{
    public class NullHealth : IHealth
    {
        public bool IsInitialized => false;

        public bool Locked { set; get; }

        public void Execute()
        {
        }

        public void Initialize()
        {
        }
    }
}
