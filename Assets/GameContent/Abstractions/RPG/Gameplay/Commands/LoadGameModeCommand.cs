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
using Assets.Abstractions.RPG.GameMode;
using Assets.Abstractions.Shared.Core.DI;

namespace Assets.Abstractions.RPG.Gameplay.Commands
{
    public class LoadGameModeCommand : ICommand<IGameMode>
    {
        private EGameMode gameMode;
        public EGameMode GameMode
        {
            set => gameMode = value;
            get => gameMode;
        }

        public static LoadGameModeCommand Create(EGameMode gameMode)
        {
            return new LoadGameModeCommand { gameMode = gameMode };
        }
    }
    public class LoadGameModeCommandHandler : ICommandHandler<LoadGameModeCommand, IGameMode>
    {
        [Inject] private IArchitecture _architecture;
        [Inject] private IGameModeLoaderServices _gameModeLoader;

        public async UniTask<IGameMode> Execute(LoadGameModeCommand command, CancellationToken cancellationToken = default)
        {
            var modeGame = command.GameMode;
            if (modeGame != EGameMode.None)
            {
                IUserGameplayData userGameplayData = null;
                if (modeGame == EGameMode.Campaign)
                {
                    userGameplayData = new CampaignUserGameplayData(modeGame, new CharacterData(CharacterType.Fighter, null, null), 1);
                }
                else if (modeGame == EGameMode.DailyDungeon)
                {
                    userGameplayData = new CampaignUserGameplayData(modeGame, null, 1);
                }
                else if (modeGame == EGameMode.Tower)
                {
                    userGameplayData = new CampaignUserGameplayData(modeGame, null, 1);
                }
                else if (modeGame == EGameMode.Survival)
                {
                    userGameplayData = new CampaignUserGameplayData(modeGame, null, 1);
                }
                var gameModeLoader = await _gameModeLoader.LoadGameMode(userGameplayData);
                await gameModeLoader.PreloadGame();

                return gameModeLoader;
            }
            return null;
        }
    }
}
