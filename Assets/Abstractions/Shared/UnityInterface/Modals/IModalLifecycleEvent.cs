using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IModalLifecycleEvent
	{
		UniTask Initialize();

		UniTask WillPushEnter();

		void DidPushEnter();

		UniTask WillPushExit();

		void DidPushExit();

		UniTask WillPopEnter();

		void DidPopEnter();

		UniTask WillPopExit();

		void DidPopExit();

		UniTask Cleanup();
	}
}
