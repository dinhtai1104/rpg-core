using Assets.Abstractions.RPG.GameServices;
using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.LocalData;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.Shared.Commands;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Assets.Abstractions.RPG.LocalData.Models;
using Assets.Abstractions.Shared.Core;
using Assets.Abstractions.Shared.Core.DI;

namespace Assets.Abstractions.RPG.Gameplay.Commands
{
    public class LoadSceneCommand : ICommand
    {
        private string sceneName;

        public string SceneName
        {
            get => sceneName;
            set => sceneName = value;
        }

        public static LoadSceneCommand Create(string sceneName) 
        {
            return new LoadSceneCommand
            {
                SceneName = sceneName,
            };
        }
    }
    public class LoadSceneCommandHandler : ICommandHandler<LoadSceneCommand>
    {
        [Inject] private IArchitecture _architecture;

        public async UniTask Execute(LoadSceneCommand command, CancellationToken cancellationToken = default)
        {
            var sceneLoader = new SceneLoader(command.SceneName);
            _architecture.Injector.Resolve(sceneLoader);
            _architecture.Injector.Resolve(command);
            sceneLoader.OnSceneLoaded += (data) =>
            {
               return _architecture.GetService<IGameSceneLoaderServices>().ActiveScene(data.Key);
            };

            await _architecture.GetService<IGameSceneLoaderServices>().LoadScene(sceneLoader);
        }
    }
}
