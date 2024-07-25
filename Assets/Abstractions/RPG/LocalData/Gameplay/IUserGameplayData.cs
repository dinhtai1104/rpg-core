using Assets.Abstractions.RPG.Misc;

namespace Assets.Abstractions.RPG.LocalData.Gameplay
{
    public interface IUserGameplayData
    {
        EGameMode GameMode { set; get; }
    }
}
