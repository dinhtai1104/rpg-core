using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.Units
{
    public abstract class BaseNullEngine : IEngine
    {
        public bool IsInitialized => false;

        public bool Locked { set; get; }
        public ICharacter Owner { set; get; }

        public void Execute()
        {
        }

        public void Initialize(ICharacter character)
        {
        }
    }
}
