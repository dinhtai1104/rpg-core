using Assets.Abstractions.Shared.Pool.Factory;
using System;

namespace Assets.Abstractions.Shared.Pool.PoolContainer
{
    /// <summary>
	/// Object pool 4 class who no public constructor
	/// such as SingletonClass.QEventSystem
	/// </summary>
	public class NonPublicObjectPool<T> : Pool<T> where T : class, IPoolable
    {
        private static NonPublicObjectPool<T> _instance;
        public static NonPublicObjectPool<T> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NonPublicObjectPool<T>();
                }
                return _instance;
            }
        }

        protected NonPublicObjectPool()
        {
            mFactory = new NonPublicObjectFactory<T>();
        }


        /// <summary>
        /// Init the specified maxCount and initCount.
        /// </summary>
        /// <param name="maxCount">Max Cache count.</param>
        /// <param name="initCount">Init Cache count.</param>
        public void Init(int maxCount, int initCount)
        {
            if (maxCount > 0)
            {
                initCount = Math.Min(maxCount, initCount);
            }

            if (CurCount >= initCount) return;

            for (var i = CurCount; i < initCount; ++i)
            {
                Recycle(mFactory.Create());
            }
        }

        /// <summary>
        /// Gets or sets the max cache count.
        /// </summary>
        /// <value>The max cache count.</value>
        public int MaxCacheCount
        {
            get { return mMaxCount; }
            set
            {
                mMaxCount = value;

                if (mCacheStack == null) return;
                if (mMaxCount <= 0) return;
                if (mMaxCount >= mCacheStack.Count) return;
                var removeCount = mMaxCount - mCacheStack.Count;
                while (removeCount > 0)
                {
                    mCacheStack.Pop();
                    --removeCount;
                }
            }
        }

        /// <summary>
        /// Allocate T instance.
        /// </summary>
        public override T Allocate()
        {
            var result = base.Allocate();
            result.IsRecycled = false;
            return result;
        }

        /// <summary>
        /// Recycle the T instance
        /// </summary>
        /// <param name="t">T.</param>
        public override bool Recycle(T t)
        {
            if (t == null || t.IsRecycled)
            {
                return false;
            }

            if (mMaxCount > 0)
            {
                if (mCacheStack.Count >= mMaxCount)
                {
                    t.OnRecycled();
                    return false;
                }
            }

            t.IsRecycled = true;
            t.OnRecycled();
            mCacheStack.Push(t);

            return true;
        }
    }

}
