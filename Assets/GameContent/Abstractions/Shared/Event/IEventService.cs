using Assets.Abstractions.Shared.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Shared.Events
{
    public interface IEventService : IService, IInitializable
    {
        int EventHandlerCount { get; }

        int EventCount { get; }

        bool Check<T>(EventHandler<IEventArgs> handler) where T : IEventArgs;

        void Subscribe<T>(EventHandler<IEventArgs> handler) where T : IEventArgs;

        void Unsubscribe<T>(EventHandler<IEventArgs> handler) where T : IEventArgs;

        void Fire(object sender, IEventArgs e);

        void FireNow(object sender, IEventArgs e);

        void FireNow<T>(object sender) where T : IEventArgs;
    }
}
