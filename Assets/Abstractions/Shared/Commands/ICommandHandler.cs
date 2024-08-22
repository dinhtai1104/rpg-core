using Cysharp.Threading.Tasks;
using System.Threading;

namespace Assets.Abstractions.Shared.Commands
{
    public interface ICommandHandler { }

    public interface ICommandHandler<in TCommand> : ICommandHandler where TCommand : ICommand
    {
        UniTask Execute(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommandHandler<in TCommand, TResponse> : ICommandHandler where TCommand : ICommand<TResponse>
    {
        UniTask<TResponse> Execute(TCommand command, CancellationToken cancellationToken = default);
    }
}
