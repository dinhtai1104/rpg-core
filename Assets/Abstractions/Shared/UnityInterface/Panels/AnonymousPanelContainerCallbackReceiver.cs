using System;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class AnonymousPanelContainerCallbackReceiver : IPanelContainerCallbackReceiver
	{
		public event Action<PanelView> OnAfterHide;
		public event Action<PanelView> OnAfterShow;
		public event Action<PanelView> OnBeforeHide;
		public event Action<PanelView> OnBeforeShow;

		public AnonymousPanelContainerCallbackReceiver(Action<PanelView> onBeforeShow = null, Action<PanelView> onAfterShow = null,
			Action<PanelView> onBeforeHide = null, Action<PanelView> onAfterHide = null)
		{
			OnBeforeShow = onBeforeShow;
			OnAfterShow = onAfterShow;
			OnBeforeHide = onBeforeHide;
			OnAfterHide = onAfterHide;
		}

		void IPanelContainerCallbackReceiver.BeforeShow(PanelView panel)
		{
			OnBeforeShow?.Invoke(panel);
		}

		void IPanelContainerCallbackReceiver.AfterShow(PanelView panel)
		{
			OnAfterShow?.Invoke(panel);
		}

		void IPanelContainerCallbackReceiver.BeforeHide(PanelView panel)
		{
			OnBeforeHide?.Invoke(panel);
		}

		void IPanelContainerCallbackReceiver.AfterHide(PanelView panel)
		{
			OnAfterHide?.Invoke(panel);
		}
	}
}
