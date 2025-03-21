using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shared.Databases
{
    public abstract class DataTableAssetBase<TDataId, TData> : DataTableAsset where TData : IData, IDataWithId<TDataId>
    {
        [SerializeField] private TData[] _entries;
        public ReadOnlyMemory<TData> Entries => _entries;
        protected abstract TDataId GetId(in TData data);
        protected sealed override void SetEntries(object obj)
        {
            if (obj is TData[] entries)
            {
                _entries = entries;
            }
            else
            {
                Debug.LogError($"{GetType().Name} error parse data to {typeof(TData).Name}[]");
            }
        }
    }
}
