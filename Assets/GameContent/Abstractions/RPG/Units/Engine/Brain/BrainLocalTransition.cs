using Abstractions.RPG.Units.Engine.Fsm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypeReferences;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Brain
{
    [System.Serializable]
    public class BrainLocalTransition
    {
        [SerializeField]
        [ClassExtends(typeof(IState))]
        private ClassTypeReference m_State;

        [SerializeField] private BrainTransition[] m_Transitions;

        public System.Type StateType
        {
            get { return m_State.Type; }
        }

        public BrainTransition[] Transitions
        {
            get { return m_Transitions; }
        }
    }
}
