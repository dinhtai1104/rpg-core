using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shared.Databases
{
    [System.Serializable]
    public class HeroData : IData, IDataWithId<int>
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;

        public int Id => _id;
        public string Name => _name;
    }
    public class HeroTable : DataTableAsset<int, HeroData>
    {
        public override string TableName => "HeroTable";

        protected override int GetId(in HeroData data)
        {
            return data.Id;
        }
    }
}
