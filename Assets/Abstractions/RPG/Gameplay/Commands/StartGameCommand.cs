using Assets.Abstractions.Shared.Commands;
using Assets.Abstractions.Shared.Core.DI;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Abstractions.RPG.Gameplay.Commands
{
    public class StartGameCommand : ICommand
    {
    }

    public class StartGameCommandHandler : ICommandHandler<StartGameCommand>
    {
        [Inject]

        public UniTask Execute(StartGameCommand command, CancellationToken cancellationToken = default)
        {
            Debug.Log($"{GetType()} Raise");
            return UniTask.CompletedTask;
        }
    }
}
