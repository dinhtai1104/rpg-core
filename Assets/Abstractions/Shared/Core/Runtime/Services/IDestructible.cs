namespace Assets.Abstractions.Shared.Core
{
    public interface IDestructible : IService
    {
        bool WillDestroy { get; set; }
        void OnWillDestroy();
    }
}