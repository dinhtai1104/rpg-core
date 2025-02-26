using Assets.Abstractions.GameScene.Core;
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
        private Dictionary<string, ViewCollection> _viewsMap;

        public async UniTask OnInitialize(IArchitecture architecture)
        {
            _views = new();
            _viewsMap = new();
            canvas = Instantiate(await _resources.GetAsync<GameObject>("UI/Canvas"), transform).GetComponent<Canvas>();
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
        }

        public bool Available(string address)
        {
            if (_viewsMap.ContainsKey(address)) return true;
            return false;
        }

        public UniTask CloseAllView(bool useTransition)
        {
            var task = new List<UniTask>();
            foreach (var view in _views)
            {
                if (view.IsVisible)
                {
                    if (useTransition)
                    {
                        task.Add(view.Hide());
                    }
                    else
                    {
                        task.Add(UniTask.RunOnThreadPool(() => view.HideNow()));
                    }
                }
            }
            return UniTask.WhenAll(task);
        }

        public UniTask CloseView(IView loading)
        {
            if (loading == null) return UniTask.CompletedTask;
            return loading.Hide();
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
    }
}
