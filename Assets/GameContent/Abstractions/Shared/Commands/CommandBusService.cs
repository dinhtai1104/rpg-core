using Assets.Abstractions.Shared.Core;
using Assets.Abstractions.Shared.Core.DI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Abstractions.Shared.Commands
{
    [Service(typeof(ICommandBusService))]
    public class CommandBusService : MonoBehaviour, ICommandBusService
    {
        [Inject] private IArchitecture _architecture;
        public int Priority => 0;
        public bool Initialized { get; set; }

        private ICommandBus _commandBus;

        public UniTask OnInitialize(IArchitecture architecture)
        {
            _commandBus = new CommandBus();
            Initialized = true;
            return UniTask.CompletedTask;
        }

        public void Register<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
        {
            _architecture.Injector.Resolve(handler);
            _commandBus.Register(handler);
        }

        public void Register<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> handler) where TCommand : ICommand<TResponse>
        {
            _architecture.Injector.Resolve(handler);
            _commandBus.Register(handler);
        }

        public void UnRegister<THandler>() where THandler : ICommandHandler
        {
            _commandBus.UnRegister<THandler>();
        }

        public UniTask Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            _architecture.Injector.Resolve(command);
            return _commandBus.Execute(command, cancellationToken);
        }

        public UniTask<TResponse> Execute<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>
        {
            _architecture.Injector.Resolve(command);
            return _commandBus.Execute<TCommand, TResponse>(command, cancellationToken);
        }

        public void Clear()
        {
            _commandBus.Clear();
        }
    }
}
