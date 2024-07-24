using System;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class ScreenViewConfigObject : ScriptableObject, IViewConfig
	{
		[SerializeField] private bool _stack;
		[SerializeField] private bool _preload;
		[SerializeField] private bool _loadAsync;
		[SerializeField] private bool _playAnimation;
		[SerializeField] private string _assetPath;
		[SerializeField] private PoolingPolicy _poolingPolicy;

		public bool Stack => _stack;
		public bool Preload => _preload;
		public bool LoadAsync => _loadAsync;
		public bool PlayAnimation => _playAnimation;
		public string AssetPath => _assetPath;
		public PoolingPolicy PoolingPolicy => _poolingPolicy;
		public void SetViewLoadedCallback(Action<IView> callback) { }
	}
}
