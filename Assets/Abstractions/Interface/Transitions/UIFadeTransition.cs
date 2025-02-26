using Assets.Abstractions.GameScene.Interface;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Interface.Transitions
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIFadeTransition : UIBaseTransition
    {
        private CanvasGroup cvg;

        public override void Init()
        {
            base.Init();
            if (!cvg)
            {
                cvg = GetComponent<CanvasGroup>();
            }
            cvg.alpha = 0;
        }

        public override async UniTask Show(CancellationToken cts)
        {
            await base.Show(cts);
            await cvg.DOFade(1, Settings.Enter.Duration).SetEase(Settings.Enter.Curve).SetDelay(Settings.Enter.Delay).ToUniTask(TweenCancelBehaviour.Complete, cts);
        }

        public override async UniTask Hide(CancellationToken cts)
        {
            await base.Hide(cts);
            await cvg.DOFade(0, Settings.Exit.Duration).SetEase(Settings.Exit.Curve).SetDelay(Settings.Exit.Delay).ToUniTask(TweenCancelBehaviour.Complete, cts);
        }
    }
}
