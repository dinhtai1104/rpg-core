using System;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IViewConfig
	{
		bool LoadAsync { get; }
		bool PlayAnimation { get; }
		string AssetPath { get; }
		PoolingPolicy PoolingPolicy { get; }
		void SetViewLoadedCallback(Action<IView> callback);
	}
}
