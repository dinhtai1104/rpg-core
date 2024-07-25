using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Abstractions.RPG.LocalData.Gameplay
{
    public class CampaignUserGameplayData : BaseUserGameplayData
    {
        private int stageId;
        private int campaignId;

        public int StageId { get => stageId; set => stageId = value; }
        public int CampaignId { get => campaignId; set => campaignId = value; }
    }
}
