using Assets.Abstractions.RPG.GameMode;
using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.Shared.Core;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Abstractions.RPG.GameServices
{
    public interface IGameModeLoaderServices : IService, IInitializable
    {
        UniTask LoadGameMode(IUserGameplayData userGameplayData);
    }

    [Service(typeof(IGameModeLoaderServices))]
    public class GameModeLoaderServices : IGameModeLoaderServices
    {
        public int Priority => 0;

        public bool Initialized { set; get; }

        private Dictionary<EGameMode, Type> _gameModeLookup;
        private IGameMode _gameModeCurrent;
        public UniTask OnInitialize(IArchitecture architecture)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var modes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => !t.IsAbstract && t.IsClass && typeof(IGameMode).IsAssignableFrom(t))
                .ToArray();

            foreach (var mode in modes)
            {
                var gameMode = (IGameMode)mode;
                _gameModeLookup.Add(gameMode.Mode, gameMode.GetType());
            }
            return UniTask.CompletedTask;
        }
        public async UniTask LoadGameMode(IUserGameplayData userGameplayData)
        {
            // load game mode
            var mode = userGameplayData.GameMode;
            _gameModeCurrent = (IGameMode)Activator.CreateInstance(_gameModeLookup[mode]);
            Log.Info("Game Mode Created! - " + _gameModeCurrent.ToString());

            await UniTask.Yield();
            // Create Loadscene

            await UniTask.Yield();
            await _gameModeCurrent.PreloadGame();

            _gameModeCurrent.Enter();
        }

    }
}
