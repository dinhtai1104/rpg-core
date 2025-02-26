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
    public class UIScaleTransition : UIBaseTransition
    {
        private RectTransform rectTransform;
        [SerializeField] private Vector2 fromScale;
        [SerializeField] private Vector2 toScale;

        public override void Init()
        {
            base.Init();
            if (!rectTransform)
            {
                rectTransform = GetComponent<RectTransform>();
            }
            rectTransform.localScale = fromScale;
        }
        public override async UniTask Hide(CancellationToken cts)
        {
            await base.Hide(cts);
            await rectTransform.DOScale(Vector3.one * fromScale, Settings.Exit.Duration).SetEase(Settings.Exit.Curve)
                      .SetDelay(Settings.Exit.Delay).ToUniTask(TweenCancelBehaviour.Complete, cts);
        }
        public override async UniTask Show(CancellationToken cts)
        {
            await base.Show(cts);
            await rectTransform.DOScale(Vector3.one * toScale, Settings.Enter.Duration).SetEase(Settings.Enter.Curve)
                .SetDelay(Settings.Enter.Delay).ToUniTask(TweenCancelBehaviour.Complete, cts);
        }
    }
}
