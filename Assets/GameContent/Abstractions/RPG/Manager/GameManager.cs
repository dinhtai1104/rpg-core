using Assets.Abstractions.RPG.GameMode;
using Assets.Abstractions.RPG.Gameplay.Commands;
using Assets.Abstractions.RPG.GameServices;
using Assets.Abstractions.RPG.Inventory;
using Assets.Abstractions.RPG.Items;
using Assets.Abstractions.RPG.Items.UsableItems;
using Assets.Abstractions.RPG.LocalData;
using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.LocalData.Models;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.Shared.Commands;
using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Assets.Abstractions.RPG.Manager
{
    public class GameManager : Architecture<GameManager>
    {
        public override bool InjectSceneLoadedDependencies => true;
        [SerializeField] private InventoryHandler inventoryHandler;

        protected override async UniTask OnInitialize()
        {
            await base.OnInitialize();
            inventoryHandler = new InventoryHandler();

            BindCommand();
        }

        private void BindCommand()
        {
            GetService<ICommandBusService>().Register(new StartGameCommandHandler());
            GetService<ICommandBusService>().Register(new LoadSceneCommandHandler());
            GetService<ICommandBusService>().Register(new LoadGameModeCommandHandler());
        }


        [Button]
        public void AddUseable(EUseable usableType, int amount, float data)
        {
            BaseRuntimeItem baseRuntimeItem = null;
            switch (usableType)
            {
                case EUseable.HealthPoison:
                    baseRuntimeItem = new HealthPoisonItem(amount, data);
                    break;
                case EUseable.Exp:
                    break;
                case EUseable.Rune:
                    break;
                case EUseable.Stat:
                    break;
                case EUseable.Invincible:
                    baseRuntimeItem = new InvincibleItem(amount, data);
                    break;
            }

            if (baseRuntimeItem == null) return;
            inventoryHandler.Add(EInventoryPack.Consumable, baseRuntimeItem);
        }

        [Button]
        public async void TestLoadScene(string sceneName, EGameMode modeGame)
        {
            await GetService<ICommandBusService>().Execute(LoadSceneCommand.Create(sceneName));
            var gameMode = await GetService<ICommandBusService>().Execute<LoadGameModeCommand, IGameMode>(LoadGameModeCommand.Create(modeGame));
            gameMode.Enter();
        }
        [Button]
        public async void TestLoadScene(string sceneName)
        {
            await GetService<ICommandBusService>().Execute(LoadSceneCommand.Create(sceneName));
        }
    }
}
