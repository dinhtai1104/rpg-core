using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Abstractions.Shared.Commands
{
    [Service(typeof(ICommandBusService))]
    public class CommandBusService : MonoBehaviour, ICommandBusService
    {
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
            _commandBus.Register(handler);
        }

        public void Register<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> handler) where TCommand : ICommand<TResponse>
        {
            _commandBus.Register(handler);
        }

        public void UnRegister<THandler>() where THandler : ICommandHandler
        {
            _commandBus.UnRegister<THandler>();
        }

        public UniTask Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            return _commandBus.Execute(command, cancellationToken);
        }

        public UniTask<TResponse> Execute<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : ICommand<TResponse>
        {
            return _commandBus.Execute<TCommand, TResponse>(command, cancellationToken);
        }

        public void Clear()
        {
            _commandBus.Clear();
        }
    }
}
