using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Abstractions.RPG.Units
{
    public class BaseMonoEngine : MonoBehaviour, IEngine
    {
        public bool IsInitialized { private set; get; }

        public bool Locked { set; get; }

        public virtual void Execute()
        {
        }

        public virtual void Initialize()
        {
            IsInitialized = true;
        }
    }
}
