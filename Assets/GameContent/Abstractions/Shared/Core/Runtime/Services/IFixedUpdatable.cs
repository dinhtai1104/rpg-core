namespace Assets.Abstractions.Shared.Core
{
    public interface IFixedUpdatable : IService
    {
        void OnFixedUpdate();
    }
}