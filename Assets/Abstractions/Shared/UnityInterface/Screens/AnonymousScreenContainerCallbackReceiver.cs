using System;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class AnonymousScreenContainerCallbackReceiver : IScreenContainerCallbackReceiver
	{
		public event Action<ScreenView, ScreenView> OnAfterPop;
		public event Action<ScreenView, ScreenView> OnAfterPush;
		public event Action<ScreenView, ScreenView> OnBeforePop;
		public event Action<ScreenView, ScreenView> OnBeforePush;

		public AnonymousScreenContainerCallbackReceiver(Action<ScreenView, ScreenView> onBeforePush = null
			, Action<ScreenView, ScreenView> onAfterPush = null
			, Action<ScreenView, ScreenView> onBeforePop = null
			, Action<ScreenView, ScreenView> onAfterPop = null)
		{
			OnBeforePush = onBeforePush;
			OnAfterPush = onAfterPush;
			OnBeforePop = onBeforePop;
			OnAfterPop = onAfterPop;
		}

		void IScreenContainerCallbackReceiver.BeforePush(ScreenView enterScreen, ScreenView exitScreen)
		{
			OnBeforePush?.Invoke(enterScreen, exitScreen);
		}

		void IScreenContainerCallbackReceiver.AfterPush(ScreenView enterScreen, ScreenView exitScreen)
		{
			OnAfterPush?.Invoke(enterScreen, exitScreen);
		}

		void IScreenContainerCallbackReceiver.BeforePop(ScreenView enterScreen, ScreenView exitScreen)
		{
			OnBeforePop?.Invoke(enterScreen, exitScreen);
		}

		void IScreenContainerCallbackReceiver.AfterPop(ScreenView enterScreen, ScreenView exitScreen)
		{
			OnAfterPop?.Invoke(enterScreen, exitScreen);
		}
	}
}
