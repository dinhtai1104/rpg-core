using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.Units
{
    public interface IEngine
    {
        ICharacter Owner { get; set; }
        bool Locked { set; get; }
        void Initialize(ICharacter character);
        void Execute();
    }
}
