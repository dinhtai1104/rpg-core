namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IScreenContainerCallbackReceiver
	{
		void BeforePush(ScreenView enterScreen, ScreenView exitScreen);

		void AfterPush(ScreenView enterScreen, ScreenView exitScreen);

		void BeforePop(ScreenView enterScreen, ScreenView exitScreen);

		void AfterPop(ScreenView enterScreen, ScreenView exitScreen);
	}
}
