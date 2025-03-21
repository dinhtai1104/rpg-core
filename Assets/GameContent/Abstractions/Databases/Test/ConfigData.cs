using BansheeGz.BGDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Shared.Databases
{
    [System.Serializable]
    public sealed class ConfigData : IDataWithId<string>, IData
    {
        [SerializeField] private string _id;
        [SerializeField] private string _value;

        public string Id => _id;
        public string Value => _value;
        public ConfigData(string id, string value)
        {
            _id = id;
            _value = value;
        }
    }
}
