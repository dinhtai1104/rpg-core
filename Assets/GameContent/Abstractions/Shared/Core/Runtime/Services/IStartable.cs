namespace Assets.Abstractions.Shared.Core
{
    public interface IStartable : IService
    {
        void OnStart();
    }
}