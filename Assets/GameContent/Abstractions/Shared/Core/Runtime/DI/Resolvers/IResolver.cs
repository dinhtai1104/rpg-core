using System;

namespace Assets.Abstractions.Shared.Core.DI
{
    public interface IResolver : IDisposable
    {
        Lifetime Lifetime { get; }
        object Resolve(Injector injector);
    }
}
