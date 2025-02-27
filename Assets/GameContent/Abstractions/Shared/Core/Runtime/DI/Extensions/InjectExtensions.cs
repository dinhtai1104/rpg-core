using System.Runtime.CompilerServices;

namespace Assets.Abstractions.Shared.Core.DI
{
    public static class InjectExtensions
    {
        private static readonly ConditionalWeakTable<Injector, DebugProperties> _containerDebugProperties = new();

        public static DebugProperties GetDebugProperties(this Injector container)
        {
            return _containerDebugProperties.GetOrCreateValue(container);
        }
    }
}
