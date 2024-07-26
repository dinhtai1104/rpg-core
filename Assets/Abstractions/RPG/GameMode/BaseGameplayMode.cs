using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.RPG.Units.Engine.Healths;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Abstractions.RPG.GameMode
{
    public abstract class BaseGameplayMode : BaseGameMode
    {
        private ICharacter mainPlayer;
        private float time = 0;
        protected ICharacter MainPlayer { get => mainPlayer; set => mainPlayer = value; }
        protected new BaseUserGameplayData UserGameplayData => (BaseUserGameplayData)base.UserGameplayData;

        public override UniTask PreloadGame()
        {
            var listTask = new List<UniTask>()
            {
                base.PreloadGame(),
                PreloadCharacter(),
            };

            return UniTask.WhenAll(listTask);
        }

        private async UniTask PreloadCharacter()
        {
            var characterPath = "Unit/" + UserGameplayData.CharacterData.Character;
            var characterObjectTask = PreLoader.PreLoad<GameObject>(characterPath);
            await characterObjectTask;
        }

        public override void Enter()
        {
            base.Enter();
            var characterPath = "Unit/" + UserGameplayData.CharacterData.Character;
            Object.Instantiate(GetAsset<GameObject>(characterPath));
        }
        public override bool EndGameCondition()
        {
            return mainPlayer.GetEngine<IHealth>().CurrentHealth == 0;
        }

        protected override void OnUpdate()
        {
            time += Time.deltaTime;
        }
    }
}
