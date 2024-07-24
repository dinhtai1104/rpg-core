namespace Assets.Abstractions.Shared.Core
{
    public interface IUpdatable : IService
    {
        void OnUpdate();
    }
}