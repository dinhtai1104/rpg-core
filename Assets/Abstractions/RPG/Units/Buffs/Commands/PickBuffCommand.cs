using Assets.Abstractions.RPG.GameServices;
using Assets.Abstractions.RPG.Units.Buffs.Misc;
using Assets.Abstractions.Shared.Commands;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Pool;
using Assets.Abstractions.Shared.Pool.PoolContainer;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Assets.Abstractions.RPG.Units.Buffs.Commands
{
    public class PickBuffCommand : ICommand, IPoolable
    {
        public EBuff buff;

        public bool IsRecycled { set; get; }

        public void OnRecycled()
        {
            IsRecycled = true;
        }
    }

    public class PickBuffCommandHandler : ICommandHandler<PickBuffCommand>
    {
        [Inject] private IResourceServices resourceService;
        public UniTask Execute(PickBuffCommand command, CancellationToken cancellationToken = default)
        {
            var buffPrefab = resourceService.Get<GameObject>($"Buffs/{command.buff}");
            Debug.Log(buffPrefab.name);
            command.OnRecycled();
            return UniTask.CompletedTask;
        }
    }
}
