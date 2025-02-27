namespace Assets.Abstractions.Shared.Core
{
    public interface ILateUpdatable : IService
    {
        void OnLateUpdate();
    }
}