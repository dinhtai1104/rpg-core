namespace Assets.Abstractions.Shared.Core
{
    public interface ISceneLoad : IService
    {
        void OnSceneLoad(string sceneName);

        void OnSceneUnload(string sceneName);
    }
}