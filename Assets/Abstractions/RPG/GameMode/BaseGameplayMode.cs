using Assets.Abstractions.RPG.Units;
using Assets.Abstractions.RPG.Units.Engine.Healths;
using UnityEngine;

namespace Assets.Abstractions.RPG.GameMode
{
    public abstract class BaseGameplayMode : BaseGameMode
    {
        private ICharacter mainPlayer;
        private float time = 0;
        protected ICharacter MainPlayer { get => mainPlayer; set => mainPlayer = value; }


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
