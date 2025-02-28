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
        public ICharacter Owner { get; set; }
        public bool IsInitialized { private set; get; }

        public virtual bool Locked { set; get; }

        public virtual void Execute()
        {
            if (Locked) return;
            OnUpdate();
        }

        protected virtual void OnUpdate()
        {

        }

        public virtual void Initialize(ICharacter character)
        {
            IsInitialized = true;
        }
    }
}
