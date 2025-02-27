using Assets.Abstractions.RPG.LocalData.Models;
using Assets.Abstractions.RPG.Misc;

namespace Assets.Abstractions.RPG.LocalData.Gameplay
{
    public class CampaignUserGameplayData : BaseUserGameplayData
    {
        private int stageId;
        private int campaignId;

        public int StageId { get => stageId; set => stageId = value; }
        public int CampaignId { get => campaignId; set => campaignId = value; }

        public CampaignUserGameplayData(EGameMode gameMode, CharacterData character, float healthPercent) : base(gameMode, character, healthPercent)
        {
        }

        public void SetData(int stage, int campaign)
        {
            this.StageId = stage;
            this.CampaignId = campaign;
        }

    }
}
