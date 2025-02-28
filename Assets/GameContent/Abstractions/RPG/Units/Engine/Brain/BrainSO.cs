using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Brain
{
    [CreateAssetMenu(fileName = "BrainSO.asset", menuName = "Brain/AI/BrainSO", order = -1)]
    public class BrainSO : ScriptableObject
    {
        [Header("Run for each entity")]
        public BrainTransition[] m_CoreTransitions;

        [Header("Run only AI Enable")]
        public BrainTransition[] m_GlobalTransitions;
        public BrainLocalTransition[] m_LocalTransitions;
    }
}
