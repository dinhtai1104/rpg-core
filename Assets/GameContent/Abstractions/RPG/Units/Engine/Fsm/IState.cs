using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.Fsm
{
    public interface IState
    {
        CharacterActor Actor { get; set; }
        void InitializeStateMachine();
        void Enter();
        void Exit();
        void Execute();
        void Reset();
    }
}
