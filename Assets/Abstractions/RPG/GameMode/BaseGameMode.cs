using Assets.Abstractions.RPG.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.GameMode
{
    public abstract class BaseGameMode : IGameMode
    {
        public EGameMode Mode;
        public bool IsEndGame { get; private set; }
        public virtual void Enter()
        {
        }
        public virtual void StartMode()
        {
        }
        public void OnExecute()
        {
            if (IsEndGame) return;
            OnUpdate();
            if (EndGameCondition())
            {
                IsEndGame = true;
                End();
            }
        }
        public virtual void End()
        {
        }
        public abstract bool EndGameCondition();
        protected abstract void OnUpdate();
    }
}
