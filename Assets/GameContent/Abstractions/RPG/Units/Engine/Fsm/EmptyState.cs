using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.RPG.Units.Engine.Fsm
{
    public class EmptyState : IState
    {
        public static readonly IState NullState = new EmptyState();
        public ICharacter Actor { get; set; }

        public void InitializeStateMachine()
        {
        }
        private EmptyState()
        {

        }

        public bool IsActive { get { return false; } }


        public void Enter()
        {
        }

        public void Execute()
        {

        }

        public void Exit()
        {

        }

        public void Reset()
        {

        }
    }
}
