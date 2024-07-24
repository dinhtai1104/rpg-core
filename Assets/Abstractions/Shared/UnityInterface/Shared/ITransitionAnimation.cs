using Assets.Abstractions.Shared.Foundation.animation;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
    public interface ITransitionAnimation : IAnimation
    {
        void SetPartner(RectTransform partnerRectTransform);
        
        void Setup(RectTransform rectTransform);
    }
}
