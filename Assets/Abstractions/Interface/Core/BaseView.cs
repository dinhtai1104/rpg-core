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
            ButtonClose.AddListener(OnClose);
        }

        private void OnClose()
        {
            this.canvasManager.CloseView(this);
        }

        public virtual UniTask PostInit(IViewModel viewModel)
        {
            LoadTransitions();
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
            canvas.enabled = true;
            graphicRaycast.enabled = true;
            await ShowAnimation();
            IsVisible = true;
        }

        public async UniTask Hide()
        {
            IsVisible = false;
            graphicRaycast.enabled = false;
            await HideAnimation().ContinueWith(()=>
            {
                canvas.enabled = false;
            });
        }

        public void HideNow()
        {
            canvas.enabled = false;
            graphicRaycast.enabled = false;
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
