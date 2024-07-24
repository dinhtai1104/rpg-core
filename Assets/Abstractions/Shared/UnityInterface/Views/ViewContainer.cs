using System;
using System.Collections.Generic;
using Assets.Abstractions.Shared.Loader.Core;
using Assets.Abstractions.Shared.Foundation;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Abstractions.Shared.UnityInterface
{
	[RequireComponent(typeof(RectMask2D), typeof(CanvasGroup))]
	public abstract class ViewContainer : View, IViewContainer
	{
		private IAssetLoader _assetLoader;
		private readonly Dictionary<string, AssetRequest<GameObject>> _assetPathToRequest = new();
		private readonly Dictionary<string, Queue<View>> _assetPathToPool = new();

		public IAssetLoader AssetLoader
		{
			get => _assetLoader ?? Settings.AssetLoader;
			set => _assetLoader = value ?? throw new ArgumentNullException(nameof(value));
		}

		protected bool EnablePooling => Settings.EnablePooling;

		protected RectTransform PoolTransform { get; private set; }

		protected virtual void InitializePool()
		{
			var parentTransform = transform.parent.GetComponent<RectTransform>();

			var poolGo = new GameObject($"[Pool] {name}", typeof(CanvasGroup), typeof(LayoutElement));
			PoolTransform = poolGo.GetOrAddComponent<RectTransform>();
			PoolTransform.SetParent(parentTransform, false);
			PoolTransform.FillParent(parentTransform);

			var poolCanvasGroup = poolGo.GetComponent<CanvasGroup>();
			poolCanvasGroup.alpha = 0f;
			poolCanvasGroup.blocksRaycasts = false;
			poolCanvasGroup.interactable = false;

			var layoutElement = poolGo.GetComponent<LayoutElement>();
			layoutElement.ignoreLayout = true;
		}

		protected override void OnDestroy()
		{
			foreach ((var assetPath, var pool) in _assetPathToPool)
			{
				while (pool.TryDequeue(out var view))
				{
					DestroyAndForget(view, assetPath, PoolingPolicy.DisablePooling).Forget();
				}
			}

			_assetPathToPool.Clear();
		}

		public int CountInPool(string assetPath)
		{
			return _assetPathToPool.TryGetValue(assetPath, out var pool) ? pool.Count : 0;
		}

		public bool ContainsInPool(string assetPath)
		{
			return _assetPathToPool.TryGetValue(assetPath, out var pool) && pool.Count > 0;
		}

		public void KeepInPool(string assetPath, int amount)
		{
			KeepInPoolAndForget(assetPath, amount).Forget();
		}

		private async UniTaskVoid KeepInPoolAndForget(string assetPath, int amount)
		{
			await KeepInPoolAsync(assetPath, amount);
		}

		public async UniTask KeepInPoolAsync(string assetPath, int amount)
		{
			if (_assetPathToPool.TryGetValue(assetPath, out var pool) == false)
			{
				return;
			}

			var amountToDestroy = pool.Count - Mathf.Clamp(amount, 0, pool.Count);
			if (amountToDestroy < 1)
			{
				return;
			}

			var doDestroying = false;
			for (var i = 0; i < amountToDestroy; i++)
			{
				if (pool.TryDequeue(out var view))
				{
					if (view && view.gameObject)
					{
						Destroy(view.gameObject);
						doDestroying = true;
					}
				}
			}

			if (doDestroying)
			{
				await UniTask.NextFrame();
			}

			if (pool.Count < 1 && _assetPathToRequest.TryGetValue(assetPath, out var request))
			{
				AssetLoader.Release(request);
				_assetPathToRequest.Remove(assetPath);
			}
		}

		public void Preload(string assetPath, bool loadAsync = true, int amount = 1)
		{
			PreloadAndForget(assetPath, loadAsync, amount).Forget();
		}

		private async UniTaskVoid PreloadAndForget(string assetPath, bool loadAsync = true, int amount = 1)
		{
			await PreloadAsync(assetPath, loadAsync, amount);
		}

		public async UniTask PreloadAsync(string assetPath, bool loadAsync = true, int amount = 1)
		{
			if (amount < 1)
			{
				Debug.LogWarning($"The amount of preloaded view instances should be greater than 0.");
				return;
			}

			if (_assetPathToPool.TryGetValue(assetPath, out var pool) == false)
			{
				_assetPathToPool[assetPath] = pool = new Queue<View>();
			}

			if (pool.Count >= amount)
			{
				return;
			}

			var assetRequest = loadAsync
				? AssetLoader.LoadAsync<GameObject>(assetPath)
				: AssetLoader.Load<GameObject>(assetPath);

			while (assetRequest.IsDone == false)
			{
				await UniTask.NextFrame();
			}

			if (assetRequest.Status == AssetRequestStatus.Failed)
			{
				throw assetRequest.OperationException;
			}

			_assetPathToRequest[assetPath] = assetRequest;

			var differentAmount = amount - pool.Count;

			for (var i = 0; i < differentAmount; i++)
			{
				InstantiateToPool(assetPath, assetRequest, pool);
			}
		}

		private void InstantiateToPool(string assetPath, AssetRequest<GameObject> assetRequest, Queue<View> pool)
		{
			var instance = Instantiate(assetRequest.Result);

			if (instance.TryGetComponent<View>(out var view) == false)
			{
				Debug.LogError($"Cannot find the {nameof(View)} component on the specified resource `{assetPath}`.", instance);
				return;
			}

			view.Settings = Settings;
			view.RectTransform.SetParent(PoolTransform);
			view.Parent = PoolTransform;
			view.Owner.SetActive(false);
			pool.Enqueue(view);
		}

		public TView GetView<TView>(IViewConfig config) where TView : View
		{
			var tcs = new UniTaskCompletionSource<TView>();
			GetViewAndForget(config, tcs).Forget();
			return tcs.GetResult(0);
		}

		private async UniTaskVoid GetViewAndForget<TView>(IViewConfig config, UniTaskCompletionSource<TView> tcs) where TView : View
		{
			var view = await GetViewAsync<TView>(config);
			tcs.TrySetResult(view);
		}

		public async UniTask<T> GetViewAsync<T>(IViewConfig config) where T : View
		{
			var assetPath = config.AssetPath;
			var loadAsync = config.LoadAsync;
			var poolingPolicy = config.PoolingPolicy;

			if (GetFromPool<T>(assetPath, poolingPolicy, out var existView))
			{
				existView.Settings = Settings;
				return existView;
			}

			AssetRequest<GameObject> assetRequest;
			var handleInMap = false;

			if (_assetPathToRequest.TryGetValue(assetPath, out var request))
			{
				assetRequest = request;
				handleInMap = true;
			}
			else
			{
				assetRequest = loadAsync
					? AssetLoader.LoadAsync<GameObject>(assetPath)
					: AssetLoader.Load<GameObject>(assetPath);
			}

			while (assetRequest.IsDone == false)
			{
				await UniTask.NextFrame();
			}

			if (assetRequest.Status == AssetRequestStatus.Failed)
			{
				throw assetRequest.OperationException;
			}

			var instance = Instantiate(assetRequest.Result);

			if (instance.TryGetComponent<T>(out var view) == false)
			{
				Debug.LogError($"Cannot find the {typeof(T).Name} component on the specified resource `{assetPath}`.", instance);
				return null;
			}

			view.Settings = Settings;

			if (handleInMap == false)
			{
				_assetPathToRequest[assetPath] = assetRequest;
			}

			return view;
		}

		protected void DestroyAndForget<T>(ViewRef<T> viewRef) where T : View
		{
			DestroyAndForget(viewRef.View, viewRef.AssetPath, viewRef.PoolingPolicy).Forget();
		}

		protected async UniTaskVoid DestroyAndForget<T>(T view, string assetPath, PoolingPolicy poolingPolicy) where T : View
		{
			if (ReturnToPool(view, assetPath, poolingPolicy))
			{
				return;
			}

			if (view && view.Owner)
			{
				Destroy(view.Owner);
				await UniTask.NextFrame();
			}

			if (ContainsInPool(assetPath))
			{
				return;
			}

			if (_assetPathToRequest.TryGetValue(assetPath, out var request))
			{
				AssetLoader.Release(request);
				_assetPathToRequest.Remove(assetPath);
			}
		}

		protected bool ReturnToPool<T>(ViewRef<T> viewRef) where T : View
		{
			return ReturnToPool(viewRef.View, viewRef.AssetPath, viewRef.PoolingPolicy);
		}

		private bool ReturnToPool<T>(T view, string assetPath, PoolingPolicy poolingPolicy) where T : View
		{
			if (view == false)
			{
				return false;
			}

			if (CanPool(poolingPolicy) == false)
			{
				return false;
			}

			if (_assetPathToPool.TryGetValue(assetPath, out var pool) == false)
			{
				_assetPathToPool[assetPath] = pool = new Queue<View>();
			}

			if (view.Owner == false)
			{
				return false;
			}

			view.RectTransform.SetParent(PoolTransform);
			view.Parent = PoolTransform;
			view.Owner.SetActive(false);
			pool.Enqueue(view);
			return true;
		}

		private bool GetFromPool<T>(string assetPath, PoolingPolicy poolingPolicy, out T view) where T : View
		{
			if (CanPool(poolingPolicy) && _assetPathToPool.TryGetValue(assetPath, out var pool) && pool.TryDequeue(out var typelessView))
			{
				if (typelessView is T typedView)
				{
					view = typedView;
					view.Settings = Settings;
					view.Owner.SetActive(true);
					return true;
				}

				if (typelessView && typelessView.gameObject)
				{
					Destroy(typelessView.Owner);
				}
			}

			view = default;
			return false;
		}

		private bool CanPool(PoolingPolicy poolingPolicy)
		{
			if (poolingPolicy == PoolingPolicy.DisablePooling)
			{
				return false;
			}

			if (poolingPolicy == PoolingPolicy.EnablePooling)
			{
				return true;
			}

			return EnablePooling;
		}
	}
}
