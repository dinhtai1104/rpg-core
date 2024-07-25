using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.GameMode
{
    public abstract class BaseGameMode : IGameMode
    {
        private IUserGameplayData userGameplayData;
        private EGameMode mode;
        public bool IsEndGame { get; private set; }
        protected IUserGameplayData UserGameplayData { get => userGameplayData; set => userGameplayData = value; }
        public EGameMode Mode { get => mode; set => mode = value; }

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

        public virtual UniTask PreloadGame()
        {
            return UniTask.CompletedTask;
        }

        public void SetData(IUserGameplayData userGameplayData)
        {
            this.UserGameplayData = userGameplayData;
        }
    }
}
