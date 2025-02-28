using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Brain
{
    [CreateAssetMenu(fileName = "DecisionGroup.asset", menuName = "AI/" + "DecisionGroup")]
    public class BrainDecisionGroup : BrainDecision
    {
        public enum BooleanOperator
        {
            AND,
            OR,
            NONE
        }

        [SerializeField] private DecisionOperator[] m_DecisionOperators;

        public override bool Decide(ICharacter actor)
        {
            var result = false;

            foreach (var decisionOperator in m_DecisionOperators)
            {
                var decision = decisionOperator.Decision.Decide(actor);
                decision = decisionOperator.IsNot ? !decision : decision;

                switch (decisionOperator.Operator)
                {
                    case BooleanOperator.NONE:
                        result = decision;
                        break;
                    case BooleanOperator.AND:
                        result = result && decision;
                        break;
                    default:
                        result = result || decision;
                        break;
                }
            }

            return result;
        }

        [System.Serializable]
        private class DecisionOperator
        {
            [SerializeField] private BrainDecision m_Decision;

            [SerializeField] private BooleanOperator m_Operator;

            [SerializeField] private bool m_IsNot;

            public BrainDecision Decision
            {
                get { return m_Decision; }
            }

            public BooleanOperator Operator
            {
                get { return m_Operator; }
            }

            public bool IsNot
            {
                get { return m_IsNot; }
            }
        }
    }
}
