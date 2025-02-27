namespace Assets.Abstractions.Shared.Core
{
    public interface ILowMemory : IService
    {
        void OnLowMemory();
    }
}