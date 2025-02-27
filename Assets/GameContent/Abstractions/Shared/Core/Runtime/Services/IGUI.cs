namespace Assets.Abstractions.Shared.Core
{
    public interface IGUI : IService
    {
        void OnGizmos();

        void OnGUI();
    }
}