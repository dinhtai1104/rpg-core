using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.Shared.Foundation;
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
        public bool IsEndGame { get; private set; }
        protected IUserGameplayData UserGameplayData { get => userGameplayData; set => userGameplayData = value; }
        public abstract EGameMode Mode { get; }
        public BaseGameMode()
        {

        }
        public virtual void Enter()
        {
            Log.Info($"{GetType().Name} Enter");
        }
        public virtual void StartMode()
        {
            Log.Info($"{GetType().Name} Start");
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
            Log.Info($"{GetType().Name} End");
        }
        public abstract bool EndGameCondition();
        protected abstract void OnUpdate();

        public virtual UniTask PreloadGame()
        {
            Log.Info($"{GetType().Name} Preloader");
            return UniTask.CompletedTask;
        }

        public void SetData(IUserGameplayData userGameplayData)
        {
            this.UserGameplayData = userGameplayData;
        }
    }
}
