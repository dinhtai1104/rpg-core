using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.Interface.Transitions
{
    [CreateAssetMenu]
    public class TransitionAnimationSettings : ScriptableObject
    {
        [SerializeField] private TransitionConfig _enter;
        [SerializeField] private TransitionConfig _exit;

        public TransitionConfig Enter { get => _enter; set => _enter = value; }
        public TransitionConfig Exit { get => _exit; set => _exit = value; }
    }
    [System.Serializable]
    public class TransitionConfig
    {
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _duration;
        [SerializeField] private float _delay;

        public AnimationCurve Curve { get => _curve; set => _curve = value; }
        public float Duration { get => _duration; set => _duration = value; }
        public float Delay { get => _delay; set => _delay = value; }
    }
}
