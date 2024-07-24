namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IModalContainerCallbackReceiver
	{
		void BeforePush(ModalView enterModal, ModalView exitModal);

		void AfterPush(ModalView enterModal, ModalView exitModal);

		void BeforePop(ModalView enterModal, ModalView exitModal);

		void AfterPop(ModalView enterModal, ModalView exitModal);
	}
}
