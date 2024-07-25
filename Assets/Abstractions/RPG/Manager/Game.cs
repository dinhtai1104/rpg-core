using Assets.Abstractions.RPG.GameServices;
using Assets.Abstractions.RPG.Inventory;
using Assets.Abstractions.RPG.Items;
using Assets.Abstractions.RPG.Items.UsableItems;
using Assets.Abstractions.RPG.LocalData;
using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;
using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.Abstractions.RPG.Manager
{
    public class Game : Architecture<Game>
    {
        public override bool InjectSceneLoadedDependencies => false;
        [SerializeField] private InventoryHandler inventoryHandler;

        protected override async UniTask OnInitialize()
        {
            await base.OnInitialize();
            inventoryHandler = new InventoryHandler();
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
        public void TestLoadScene(string sceneName, EGameMode modeGame)
        {
            var sceneLoader = new SceneLoaderData(sceneName);
            if (modeGame != EGameMode.None)
            {
                IUserGameplayData userGameplayData = null;
                if (modeGame == EGameMode.Campaign)
                {
                    userGameplayData = new CampaignUserGameplayData(modeGame, null, 1);
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
                sceneLoader.OnSceneLoaded += async (data) =>
                {
                    var gameModeServices = GetService<IGameModeLoaderServices>();
                    var gameModeLoader = await gameModeServices.LoadGameMode(userGameplayData);

                    await gameModeLoader.PreloadGame();
                    gameModeLoader.Enter();

                    data.ActiveScene();
                };

                GetService<IGameSceneLoaderServices>().LoadScene(sceneLoader, this.destroyCancellationToken);
            }
        }
    }
}
