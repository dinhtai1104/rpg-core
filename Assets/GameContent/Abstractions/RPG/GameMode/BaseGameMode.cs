using Assets.Abstractions.RPG.GameServices;
using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Manager;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.Shared.Core;
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
        private IResourceServices resourceServices;
        private GamePreloader preLoader;

        public abstract EGameMode Mode { get; }
        protected IResourceServices ResourceServices { get => resourceServices; set => resourceServices = value; }
        protected GamePreloader PreLoader { get => preLoader; set => preLoader = value; }
        protected IUserGameplayData UserGameplayData { get => userGameplayData; set => userGameplayData = value; }


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
            resourceServices = GetArchitecture().GetService<IResourceServices>();
            preLoader = new GamePreloader(resourceServices);
        }

        public TAsset GetAsset<TAsset>(string path) where TAsset : UnityEngine.Object
        {
            return preLoader.GetAsset<TAsset>(path);
        }

        public IArchitecture GetArchitecture()
        {
            return Game.Instance;
        }
    }
}
