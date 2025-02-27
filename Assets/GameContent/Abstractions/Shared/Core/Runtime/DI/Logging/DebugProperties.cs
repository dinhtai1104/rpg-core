using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Core.DI
{
    public sealed class DebugProperties
    {
        public List<CallSite> BuildCallsite { get; } = new();
    }
}
