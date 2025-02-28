using Assets.Abstractions.RPG.GameMode;
using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.Shared.Core;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Assets.Abstractions.RPG.GameServices
{
    public interface IGameModeLoaderServices : IService, IInitializable
    {
        UniTask<IGameMode> LoadGameMode(IUserGameplayData userGameplayData, CancellationToken token = default);
    }

    [Service(typeof(IGameModeLoaderServices))]
    public class GameModeLoaderServices : IGameModeLoaderServices
    {
        public int Priority => 0;

        public bool Initialized { set; get; }

        private Dictionary<EGameMode, Type> _gameModeLookup;
        private List<Type> _gameModeList;
        [Inject] private IInjector _injector;
        public UniTask OnInitialize(IArchitecture architecture)
        {
            _gameModeList = new();
            _gameModeLookup = new();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var modes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => !t.IsAbstract && t.IsClass && typeof(IGameMode).IsAssignableFrom(t))
                .ToArray();

            foreach (var mode in modes)
            {
                var ins = (IGameMode)Activator.CreateInstance(mode);

                //_gameModeList.Add(mode);
                //var gameMode = ((IGameMode)mode).Mode;
                _gameModeLookup.Add(ins.Mode, mode);
            }
            return UniTask.CompletedTask;
        }
        public async UniTask<IGameMode> LoadGameMode(IUserGameplayData userGameplayData, CancellationToken token = default)
        {
            await UniTask.Delay(0);
            // load game mode
            var mode = userGameplayData.GameMode;
            var _gameModeCurrent = (IGameMode)Activator.CreateInstance(_gameModeLookup[mode]);
            _injector.Resolve(_gameModeCurrent);

            _gameModeCurrent.SetData(userGameplayData);
            Log.Info("Game Mode Created! - " + _gameModeCurrent.ToString());
            return _gameModeCurrent;
        }

    }
}
