﻿using Assets.Abstractions.GameScene.Core;
using Assets.Abstractions.GameScene.Interface;
using Assets.Abstractions.Interface.Message;
using Assets.Abstractions.RPG.GameServices;
using Assets.Abstractions.Shared.Core;
using Assets.Abstractions.Shared.Core.DI;
using Assets.Abstractions.Shared.Core.Manager;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.GameScene
{
    public interface ICanvasManager : IService, IInitializable, IUpdatable, IManager
    {
        UniTask<IView> ShowView(IViewModel viewModal);
        bool Available(string address);
        UniTask CloseAllView(bool useTransition);
        UniTask CloseView(IView loading);
        UniTask CloseTopView();
    }

    public class ViewCollection
    {
        public string address;
        public List<IView> views;

        public ViewCollection(string address)
        {
            this.address = address;
            views = new();
        }
    }
    [Service(typeof(ICanvasManager))]
    public class CanvasManager : MonoBehaviour, ICanvasManager
    {
        public bool Initialized { set; get; }
        public int Priority => 100;

        public bool IsInitialized { get; private set; }

        private int currentOrder = 999;
        private Canvas canvas;
        [Inject] private IResourceServices _resources;
        [Inject] private IInjector _injector;
        private List<IView> _views;
        private List<IView> _viewsVisible;
        private Dictionary<string, ViewCollection> _viewsMap;

        public async UniTask OnInitialize(IArchitecture architecture)
        {
            _views = new();
            _viewsMap = new();
            _viewsVisible = new();
            canvas = Instantiate(await _resources.GetAsync<GameObject>("UI/Canvas"), transform).GetComponent<Canvas>();
        }

        private void AddViewVisible(IView view)
        {
            _viewsVisible.Add(view);
        }
        
        private void RemoveViewVisible(IView view)
        {
            _viewsVisible.Remove(view);
        }

        public async UniTask<IView> ShowView(IViewModel viewModal)
        {
            var address = viewModal.Addressable;

            if (Available(address, out var view))
            {
                _injector.Resolve(viewModal);
                view.Order = GetNextOrder();
                await view.PostInit(viewModal);
                await view.Show();

                AddViewVisible(view);
                return view;
            }

            var task = await _resources.GetAsync<GameObject>(address);
            if (task != null)
            {
                _injector.Resolve(viewModal);

                var viewInstance = Instantiate(task.GetComponent<BaseView>(), canvas.transform);
                viewInstance.Initialize(this);
                _injector.Resolve(viewInstance);
                viewInstance.Order = GetNextOrder();
                await viewInstance.PostInit(viewModal);
                await viewInstance.Show();
                AddViewVisible(viewInstance);

                _views.Add(viewInstance);
                if (!_viewsMap.ContainsKey(address))
                {
                    _viewsMap.Add(address, new(address));
                }
                _viewsMap[address].views.Add(viewInstance);
                return viewInstance;
            }
            return null;
        }

        public bool Available(string address, out IView view)
        {
            view = null;
            if (_viewsMap.TryGetValue(address, out var collection))
            {
                foreach (var v in collection.views)
                {
                    if (!v.IsVisible)
                    {
                        view = v;
                        return true;
                    }
                }
            }
            return false;
        }

        public int GetNextOrder()
        {
            currentOrder++;
            return currentOrder;
        }


        public void OnUpdate()
        {
            foreach (var view in _viewsVisible)
            {
                view.OnUpdate();
            }
        }

        public bool Available(string address)
        {
            if (_viewsMap.ContainsKey(address)) return true;
            return false;
        }

        public UniTask CloseAllView(bool useTransition)
        {
            var task = new List<UniTask>();
            for (int i = _viewsVisible.Count - 1; i >= 0; i--)
            {
                var view = _viewsVisible[i];
                if (useTransition)
                {
                    task.Add(CloseView(view));
                }
                else
                {
                    task.Add(UniTask.RunOnThreadPool(() => view.HideNow()));
                }
            }
            return UniTask.WhenAll(task);
        }

        public UniTask CloseView(IView view)
        {
            if (view == null) return UniTask.CompletedTask;
            RemoveViewVisible(view);

            return view.Hide();
        }

        public async UniTask Initialize()
        {
            IsInitialized = true;
        }

        public void Dispose()
        {
        }

        [Button]
        public void ShowMessage(string text)
        {
            ShowView(new MessagePanelModel() { Addressable = "UI/UIMessagePanel", Message = text }).Forget();
        }

        public async UniTask CloseTopView()
        {
            if (_viewsVisible.Count > 0)
            {
                var view = _viewsVisible[0];
                await CloseView(view);
            }
        }
    }
}
