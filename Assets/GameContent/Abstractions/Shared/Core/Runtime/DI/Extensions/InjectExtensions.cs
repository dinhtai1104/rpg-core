using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Abstractions.Shared.Core.DI
{
    public static class InjectExtensions
    {
        private static readonly ConditionalWeakTable<Injector, DebugProperties> _containerDebugProperties = new();

        public static DebugProperties GetDebugProperties(this Injector container)
        {
            return _containerDebugProperties.GetOrCreateValue(container);
        }

        public static void ResolveGameObject(this IInjector container, GameObject gameObject)
        {
            var monos = gameObject.GetComponentsInChildren<MonoBehaviour>();
            foreach (var mono in monos)
            {
                container.Resolve(mono);
            }
        }
    }
}
