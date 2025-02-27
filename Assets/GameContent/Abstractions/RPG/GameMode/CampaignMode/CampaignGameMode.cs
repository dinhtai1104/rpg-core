using Assets.Abstractions.RPG.LocalData.Gameplay;
using Assets.Abstractions.RPG.Misc;

namespace Assets.Abstractions.RPG.GameMode.CampaignMode
{
    public class CampaignGameMode : BaseGameplayMode
    {
        public override EGameMode Mode => EGameMode.Campaign;
        protected new CampaignUserGameplayData UserGameplayData => (CampaignUserGameplayData)base.UserGameplayData;

        public CampaignGameMode() : base()
        {
        }

        protected override void OnUpdate()
        {
        }
    }
}
