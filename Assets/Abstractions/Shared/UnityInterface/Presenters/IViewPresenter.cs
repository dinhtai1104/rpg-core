using System;

namespace Assets.Abstractions.Shared.UnityInterface
{
	public interface IViewPresenter : IDisposable
	{
		bool IsDisposed { get; }

		bool IsInitialized { get; }
        
		IViewConfig ViewConfig { get; }

		IViewContainer ViewContainer { get; }
	}
}
