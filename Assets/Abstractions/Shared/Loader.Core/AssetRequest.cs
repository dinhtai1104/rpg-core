using System;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

namespace Assets.Abstractions.Shared.Loader.Core
{
	public abstract class AssetRequest
	{
		public int RequestId { private set; get; }

		public bool IsDone => Status != AssetRequestStatus.None;

		public AssetRequestStatus Status { get; protected set; }

		public float Progress => ProgressFunc.Invoke();

		public Exception OperationException { get; protected set; }

		protected Func<float> ProgressFunc;

		protected AssetRequest(int requestId)
		{
			RequestId = requestId;
		}
	}

	public sealed class AssetRequest<TAsset> : AssetRequest, IAssetRequest<TAsset> where TAsset : Object
	{
		public TAsset Result { get; private set; }
		public UniTask<TAsset> Task { get; private set; }

		public AssetRequest(int requestId) : base(requestId) { }

		void IAssetRequest<TAsset>.SetStatus(AssetRequestStatus status)
		{
			Status = status;
		}

		void IAssetRequest<TAsset>.SetResult(TAsset result)
		{
			Result = result;
		}

		void IAssetRequest<TAsset>.SetProgressFunc(Func<float> progressFunc)
		{
			ProgressFunc = progressFunc;
		}

		void IAssetRequest<TAsset>.SetTask(UniTask<TAsset> task)
		{
			Task = task;
		}

		void IAssetRequest<TAsset>.SetOperationException(Exception ex)
		{
			OperationException = ex;
		}
	}
}
