using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assets.Abstractions.GameScene.Interface
{
    public interface IAnimationTransition
    {
        UniTask Show(CancellationToken cts);
        UniTask Hide(CancellationToken cts);
    }
}
