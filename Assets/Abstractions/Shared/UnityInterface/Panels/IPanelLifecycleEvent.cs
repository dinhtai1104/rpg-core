using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IPanelLifecycleEvent
	{
		UniTask Initialize();

		UniTask WillEnter();

		void DidEnter();

		UniTask WillExit();

		void DidExit();

		UniTask Cleanup();
	}
}
