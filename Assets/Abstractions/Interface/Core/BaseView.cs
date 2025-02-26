using Assets.Abstractions.GameScene.Interface;
using Assets.Abstractions.Interface.Core;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Abstractions.GameScene.Core
{
    public class BaseView : MonoBehaviour, IView
    {
        private Canvas canvas;
        private GraphicRaycaster graphicRaycast;
        private CancellationTokenSource cts;
        private IViewModel viewModal;
        private List<IAnimationTransition> _transitions;
        private ICanvasManager canvasManager;
        private int order;
        [SerializeField] private UIBaseButton _buttonClose;
        public UIBaseButton ButtonClose => _buttonClose;

        public CancellationToken CancellationToken => cts.Token;
        public IViewModel ViewModel => viewModal;
        public bool IsVisible { set; get; } = false;


        public int Order
        {
            get 
            {
                return order;
            }
            set
            {
                order = value;
                canvas.sortingOrder = order;
            }
        }

        /// <summary>
        /// init when first loading view
        /// </summary>
        public void Initialize(ICanvasManager manager)
        {
            canvasManager = manager;
            canvas = GetComponent<Canvas>();
            graphicRaycast = GetComponent<GraphicRaycaster>();
            if (ButtonClose)
                ButtonClose.AddListener(OnClose);
            LoadTransitions();
            graphicRaycast.enabled = false;
        }

        private void OnClose()
        {
            this.canvasManager.CloseView(this);
        }

        public virtual UniTask PostInit(IViewModel viewModel)
        {
            graphicRaycast.enabled = false;
            foreach (var trans in _transitions)
            {
                trans.Init();
            }
            cts = new();
            this.viewModal = viewModel;
            return UniTask.CompletedTask;
        }

        private void LoadTransitions()
        {
            if (_transitions != null) return;
            _transitions = new(GetComponentsInChildren<IAnimationTransition>());
        }

        public async UniTask Show()
        {
            // play animation
            gameObject.SetActive(true);
            await ShowAnimation();
            graphicRaycast.enabled = true;
            IsVisible = true;
        }

        public async UniTask Hide()
        {
            IsVisible = false;
            graphicRaycast.enabled = false;
            await HideAnimation().ContinueWith(()=>
            {
                gameObject.SetActive(false);
            });
        }

        public void HideNow()
        {
            graphicRaycast.enabled = false;
            gameObject.SetActive(false);
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnClosed()
        {

        }

        #region Animation
        private async UniTask ShowAnimation()
        {
            var list = new List<UniTask>();
            foreach (var trans in _transitions)
            {
                trans.Init();
            }
            foreach (var trans in _transitions)
            {
                list.Add(trans.Show(CancellationToken));
            }
            await UniTask.WhenAll(list).AttachExternalCancellation(CancellationToken);
        }

        private async UniTask HideAnimation()
        {
            var list = new List<UniTask>();
            foreach (var trans in _transitions)
            {
                list.Add(trans.Hide(CancellationToken));
            }
            await UniTask.WhenAll(list).AttachExternalCancellation(CancellationToken);
        }
        #endregion
    }
}
