// using System;
//
// namespace Assets.Abstractions.Shared.UnityInterface
// {
// 	public static class PanelContainerExtensions
// 	{
// 		public static void AddCallbackReceiver(this PanelContainer self, Action<PanelView> onBeforeShow = null, Action<PanelView> onAfterShow = null,
// 			Action<PanelView> onBeforeHide = null, Action<PanelView> onAfterHide = null)
// 		{
// 			var callbackReceiver = new AnonymousPanelContainerCallbackReceiver(onBeforeShow, onAfterShow, onBeforeHide, onAfterHide);
// 			self.AddCallbackReceiver(callbackReceiver);
// 		}
//
// 		public static void AddCallbackReceiver(this PanelContainer self, PanelView panel, Action<PanelView> onBeforePush = null,
// 			Action<PanelView> onAfterPush = null, Action<PanelView> onBeforePop = null, Action<PanelView> onAfterPop = null)
// 		{
// 			var callbackReceiver = new AnonymousPanelContainerCallbackReceiver();
//
// 			callbackReceiver.OnBeforeShow += x =>
// 			{
// 				if (x.Equals(panel))
// 				{
// 					onBeforePush?.Invoke(panel);
// 				}
// 			};
//
// 			callbackReceiver.OnAfterShow += x =>
// 			{
// 				if (x.Equals(panel))
// 				{
// 					onAfterPush?.Invoke(x);
// 				}
// 			};
//
// 			callbackReceiver.OnBeforeHide += x =>
// 			{
// 				if (x.Equals(panel))
// 				{
// 					onBeforePop?.Invoke(x);
// 				}
// 			};
//
// 			callbackReceiver.OnAfterHide += x =>
// 			{
// 				if (x.Equals(panel))
// 				{
// 					onAfterPop?.Invoke(x);
// 				}
// 			};
//
// 			var gameObj = self.gameObject;
//
// 			if (gameObj.TryGetComponent<MonoBehaviourDestroyedEventDispatcher>(out var destroyedEventDispatcher) == false)
// 			{
// 				destroyedEventDispatcher = gameObj.AddComponent<MonoBehaviourDestroyedEventDispatcher>();
// 			}
//
// 			destroyedEventDispatcher.OnDispatch += () => self.RemoveCallbackReceiver(callbackReceiver);
//
// 			self.AddCallbackReceiver(callbackReceiver);
// 		}
// 	}
// }
