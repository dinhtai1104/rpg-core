namespace Assets.Abstractions.Shared.UnityInterface
{
    public interface IPanelContainerCallbackReceiver
    {
        void BeforeShow(PanelView panel);

        void AfterShow(PanelView panel);

        void BeforeHide(PanelView panel);

        void AfterHide(PanelView panel);
    }
}
