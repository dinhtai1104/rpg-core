using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shared.Databases
{
    public abstract class DataTableAsset : ScriptableObject, IDisposable
    {
        public abstract string TableName { get; }
        protected abstract void SetEntries(object obj);
        public virtual void Initialize()
        {

        }
        public virtual void Dispose()
        {
        }
    }

    public abstract class DataTableAsset<TDataId, TData> : DataTableAssetBase<TDataId, TData> where TData : IData, IDataWithId<TDataId>
    {
        private readonly Dictionary<TDataId, int> _idToIndexMap;
        protected Dictionary<TDataId, int> IdToIndexMap => _idToIndexMap;
        public override void Initialize()
        {
            base.Initialize();
        }
        public virtual bool TryGetEntry(TDataId id, out TData entry)
        {
            entry = default(TData);
            if (_idToIndexMap.TryGetValue(id, out var indexMap))
            {
                entry = Entries.Span[indexMap];
                return true;
            }
            return false;
        }
        protected virtual string ToString(TDataId value)
        {
            return string.Empty;
        }
    }

    public abstract class DataTableAsset<TDataId, TData, TConvertedId> : DataTableAssetBase<TDataId, TData> where TData : IData, IDataWithId<TDataId>
    {
        private readonly Dictionary<TConvertedId, int> _idToIndexMap;
        protected Dictionary<TConvertedId, int> IdToIndexMap => _idToIndexMap;
        public override void Initialize()
        {
            base.Initialize();
        }
        public virtual bool TryGetEntry(TConvertedId id, out TData entry)
        {
            entry = default(TData);
            if (_idToIndexMap.TryGetValue(id, out var indexMap))
            {
                entry = Entries.Span[indexMap];
                return true;
            }
            return false;
        }
        protected abstract TConvertedId Convert(TDataId value);
        protected virtual string ToString(TConvertedId value)
        {
            return string.Empty;
        }

    }
}
