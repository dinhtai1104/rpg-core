using System;
using UnityEngine;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public class ViewConfigObject : ScriptableObject, IViewConfig
	{
		[SerializeField] private bool _preload = false;
		[SerializeField] private bool _loadAsync = true;
		[SerializeField] private bool _playAnimation = true;
		[SerializeField] private string _assetPath;
		[SerializeField] private PoolingPolicy _poolingPolicy = PoolingPolicy.UseSettings;

		public bool Preload => _preload;
		public bool LoadAsync => _loadAsync;
		public bool PlayAnimation => _playAnimation;
		public string AssetPath => _assetPath;
		public PoolingPolicy PoolingPolicy => _poolingPolicy;
		
		public void SetViewLoadedCallback(Action<IView> callback) { }
	}
}
