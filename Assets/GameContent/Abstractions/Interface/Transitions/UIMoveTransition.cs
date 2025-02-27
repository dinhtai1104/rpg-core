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
    public class UIMoveTransition : UIBaseTransition
    {
        private RectTransform rectTransform;
        [SerializeField] private Vector2 fromAnchor;
        private Vector2 beginAnchor;

        public override void Init()
        {
            base.Init();
            if (!rectTransform)
            {
                rectTransform = GetComponent<RectTransform>();
                beginAnchor = rectTransform.anchoredPosition;
            }
            rectTransform.anchoredPosition = beginAnchor + fromAnchor;
        }
        public override async UniTask Show(CancellationToken cts)
        {
            await base.Show(cts);
            await rectTransform.DOAnchorPos(beginAnchor, Settings.Enter.Duration).SetEase(Settings.Enter.Curve)
                .SetDelay(Settings.Enter.Delay).ToUniTask(TweenCancelBehaviour.Complete, cts);
        }
        public override async UniTask Hide(CancellationToken cts)
        {
            await base.Hide(cts);
            await rectTransform.DOAnchorPos(beginAnchor + fromAnchor, Settings.Exit.Duration).SetEase(Settings.Exit.Curve)
                      .SetDelay(Settings.Exit.Delay).ToUniTask(TweenCancelBehaviour.Complete, cts);
        }
    }
}
