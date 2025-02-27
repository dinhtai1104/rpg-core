namespace Assets.Abstractions.Shared.Core
{
    public interface IPausable : IService
    {
        void OnAppPause(bool pause);
    }
}