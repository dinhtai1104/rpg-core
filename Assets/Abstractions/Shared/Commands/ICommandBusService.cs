using Assets.Abstractions.Shared.Core;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Assets.Abstractions.Shared.Commands
{
    public interface ICommandBusService : IService, IInitializable
    {
        void Register<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand;
        void Register<TCommand, TResponse>(ICommandHandler<TCommand, TResponse> handler) where TCommand : ICommand<TResponse>;
        void UnRegister<THandler>() where THandler : ICommandHandler;
        UniTask Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand;
        UniTask<TResponse> Execute<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResponse>;
        void Clear();
    }
}
