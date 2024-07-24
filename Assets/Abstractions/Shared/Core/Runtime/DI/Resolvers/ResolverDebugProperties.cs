using System.Collections.Generic;

namespace Assets.Abstractions.Shared.Core.DI
{
    public sealed class ResolverDebugProperties
    {
        public int Resolutions;
        public List<(object, List<CallSite>)> Instances { get; } = new();
        public List<CallSite> BindingCallsite { get; } = new();
    }
}
