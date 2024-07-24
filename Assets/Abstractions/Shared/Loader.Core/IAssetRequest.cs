using System;
using Cysharp.Threading.Tasks;

namespace Assets.Abstractions.Shared.Loader.Core
{
	public interface IAssetRequest<TAsset>
	{
		void SetStatus(AssetRequestStatus status);

		void SetResult(TAsset result);

		void SetProgressFunc(Func<float> progress);

		void SetTask(UniTask<TAsset> task);

		void SetOperationException(Exception ex);
	}
}
