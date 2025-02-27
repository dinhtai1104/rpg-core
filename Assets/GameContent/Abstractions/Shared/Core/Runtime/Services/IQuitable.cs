namespace Assets.Abstractions.Shared.Core
{
    public interface IQuitable : IService
    {
        void OnAppQuit();
    }
}
