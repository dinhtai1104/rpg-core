using Assets.Abstractions.Shared.Core;

namespace Assets.Abstractions.Shared.Commands
{
    public interface ICommand { }
    public interface ICommand<out TResponse> : ICommand { }
}
