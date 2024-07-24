using System;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class AnonymousModalContainerCallbackReceiver : IModalContainerCallbackReceiver
	{
		public event Action<ModalView, ModalView> OnAfterPop;
		public event Action<ModalView, ModalView> OnAfterPush;
		public event Action<ModalView, ModalView> OnBeforePop;
		public event Action<ModalView, ModalView> OnBeforePush;

		public AnonymousModalContainerCallbackReceiver(Action<ModalView, ModalView> onBeforePush = null,
			Action<ModalView, ModalView> onAfterPush = null,
			Action<ModalView, ModalView> onBeforePop = null,
			Action<ModalView, ModalView> onAfterPop = null)
		{
			OnBeforePush = onBeforePush;
			OnAfterPush = onAfterPush;
			OnBeforePop = onBeforePop;
			OnAfterPop = onAfterPop;
		}

		void IModalContainerCallbackReceiver.BeforePush(ModalView enterModal, ModalView exitModal)
		{
			OnBeforePush?.Invoke(enterModal, exitModal);
		}

		void IModalContainerCallbackReceiver.AfterPush(ModalView enterModal, ModalView exitModal)
		{
			OnAfterPush?.Invoke(enterModal, exitModal);
		}

		void IModalContainerCallbackReceiver.BeforePop(ModalView enterModal, ModalView exitModal)
		{
			OnBeforePop?.Invoke(enterModal, exitModal);
		}

		void IModalContainerCallbackReceiver.AfterPop(ModalView enterModal, ModalView exitModal)
		{
			OnAfterPop?.Invoke(enterModal, exitModal);
		}
	}
}
