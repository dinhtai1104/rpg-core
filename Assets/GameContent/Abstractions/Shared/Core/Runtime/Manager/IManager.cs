using System;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.Core.Manager
{
	public interface IManager : IDisposable
	{
		int Priority { get; }

		bool IsInitialized { get; }

		UniTask Initialize();
	}
}
