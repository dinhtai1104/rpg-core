using Assets.Abstractions.RPG.LocalData.Models;
using Assets.Abstractions.RPG.Misc;

namespace Assets.Abstractions.RPG.LocalData.Gameplay
{
    public class BaseUserGameplayData : IUserGameplayData
    {
        private CharacterData characterData;
        private EGameMode gameMode = EGameMode.Campaign;

        private float healthPercent = 1f;

        public CharacterData CharacterData { get => characterData; set => characterData = value; }
        public EGameMode GameMode { get => gameMode; set => gameMode = value; }
        public float HealthPercent { get => healthPercent; set => healthPercent = value; }

        public BaseUserGameplayData(EGameMode gameMode, CharacterData character, float healthPercent)
        {
            this.gameMode = gameMode;
            CharacterData = character;
            HealthPercent = healthPercent;
        }
    }
}
