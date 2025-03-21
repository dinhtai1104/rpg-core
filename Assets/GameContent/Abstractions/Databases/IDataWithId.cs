using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared.Databases
{
    public interface IDataWithId<out TDataId> : IData
    {
        TDataId Id { get; }
    }
}