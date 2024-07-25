using Assets.Abstractions.RPG.LocalData.Gameplay;

namespace Assets.Abstractions.RPG.GameMode.CampaignMode
{
    public class CampaignGameMode : BaseGameplayMode
    {
        protected new CampaignUserGameplayData UserGameplayData => (CampaignUserGameplayData)base.UserGameplayData;

        protected override void OnUpdate()
        {
        }
    }
}
