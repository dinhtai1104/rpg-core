using Assets.Abstractions.RPG.Units;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Graphics
{
    public class GraphicEngine : MonoBehaviour, IGraphicEngine
    {
        [SerializeField] private bool m_GraphicAlphaOnStart;
        [SerializeField, Range(0, 1f)] private float m_GraphicAlpha = 1f;

        private const string ShaderFillColor = "_FillColor";
        private const string ShaderFillPhase = "_FillPhase";

        private static readonly int Color = Shader.PropertyToID(ShaderFillColor);
        private static readonly int FlashAmount = Shader.PropertyToID(ShaderFillPhase);

        private Renderer[] m_Renderers;
        private MaterialPropertyBlock m_PropBlock;
        private Tweener m_FlashColorTween;

        public CharacterActor Owner { get; private set; }

        public bool IsInitialized { get; set; }

        public bool Locked { set; get; }

        private void Awake()
        {
            m_Renderers = GetComponentsInChildren<Renderer>();
        }

        private void Start()
        {
            if (m_GraphicAlphaOnStart)
            {
                SetGraphicAlpha(m_GraphicAlpha);
            }
        }

        public void Init(CharacterActor actor)
        {
            Owner = actor;
            m_PropBlock = new MaterialPropertyBlock();
        }

        public void SetActiveRenderer(bool active)
        {
            foreach (var r in m_Renderers)
            {
                r.enabled = active;
            }
        }

        public void SetGraphicAlpha(float a)
        {
           
        }

        public void SetFlashAmount(float amount)
        {
            foreach (var r in m_Renderers)
            {
                r.GetPropertyBlock(m_PropBlock);
            }

            m_PropBlock.SetFloat(FlashAmount, amount);

            foreach (var r in m_Renderers)
            {
                r.SetPropertyBlock(m_PropBlock);
            }
        }

        public void FlashColor(float flickerDuration, int flickerAmount)
        {
            m_FlashColorTween?.Kill();
            m_FlashColorTween = DOTween.To(SetFlashAmount, 0f, 1f, flickerDuration)
                .OnComplete(() => { SetFlashAmount(0f); }).SetLoops(flickerAmount);
        }

        public void ClearFlashColor()
        {
            SetFlashAmount(0f);
        }

        public void Initialize()
        {
        }

        public void Execute()
        {
        }
    }
}
