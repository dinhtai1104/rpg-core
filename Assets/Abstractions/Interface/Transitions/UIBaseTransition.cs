using Assets.Abstractions.GameScene.Interface;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Interface.Transitions
{
    public class UIBaseTransition : MonoBehaviour, IAnimationTransition
    {
        [SerializeField] private TransitionAnimationSettings _settings;
        public TransitionAnimationSettings Settings => _settings;
        public virtual void Init()
        {
        }

        public virtual UniTask Hide(CancellationToken cts)
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask Show(CancellationToken cts)
        {
            return UniTask.CompletedTask;
        }
    }
}
