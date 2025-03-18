using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnRegister
{
    public static class UnRegisterExtension
    {
        public static IUnRegister UnRegisterWhenGameObjectDestroyed(this IUnRegister unRegister, GameObject gameObject)
        {
            var trigger = gameObject.GetComponent<UnRegisterOnDestroyTrigger>();

            if (!trigger)
            {
                trigger = gameObject.AddComponent<UnRegisterOnDestroyTrigger>();
            }

            trigger.AddUnRegister(unRegister);
            return unRegister;
        }

        public static UniTask UnRegister(this UniTask disposable, MonoBehaviour gameObject)
        {
            var reg = gameObject.GetOrAddComponent<UniTaskUnRegisterByDisable>();
            if (reg != null)
            {
                return disposable.AttachExternalCancellation(reg.Register().Token);
            }
            return disposable;
        }

        public static UniTask<T> UnRegister<T>(this UniTask<T> disposable, MonoBehaviour gameObject)
        {
            var reg = gameObject.GetOrAddComponent<UniTaskUnRegisterByDisable>();
            if (reg != null)
            {
                return disposable.AttachExternalCancellation(reg.Register().Token);
            }
            return disposable;
        }
    }
}