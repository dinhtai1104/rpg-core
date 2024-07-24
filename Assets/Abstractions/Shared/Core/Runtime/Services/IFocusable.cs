namespace Assets.Abstractions.Shared.Core
{
    public interface IFocusable : IService
    {
        void OnAppFocus(bool focus);
    }
}