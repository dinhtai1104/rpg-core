using Abstractions.RPG.Units.Engine.Fsm;
using Assets.Abstractions.RPG.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Abstractions.RPG.Units.Engine.Brain
{
    public class BrainEngine : BaseMonoEngine, IBrainEngine
    {
        [SerializeField] private float m_UpdateInterval = 0.1f;
        [SerializeField] private BrainSO m_Brain;

        private float m_AiTimer;

        public BrainTransition[] CoreTransitions
        {
            get { return m_Brain.m_CoreTransitions; }
        }

        public BrainTransition[] GlobalTransitions
        {
            get { return m_Brain.m_GlobalTransitions; }
        }


        public BrainLocalTransition[] LocalTransitions
        {
            get { return m_Brain.m_LocalTransitions; }
        }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            m_AiTimer += Time.deltaTime;
            if (!(m_AiTimer >= m_UpdateInterval))
            {
                return;
            }

            m_AiTimer = 0f;
            bool coreResult = false;
            if (CoreTransitions != null)
            {
                coreResult = CheckGlobalTransitions(CoreTransitions);
            }

            if (!coreResult && Owner.AI)
            {
                bool globalResult = false;
                if (GlobalTransitions != null)
                {
                    globalResult = CheckGlobalTransitions(GlobalTransitions);
                }

                if (globalResult || LocalTransitions == null)
                {
                    return;
                }

                foreach (var local in LocalTransitions)
                {
                    if (!Owner.GetEngine<IFsm>().IsCurrentState(local.StateType)) continue;
                    CheckLocalTransitions(local.Transitions);
                    break;
                }
            }
        }

        private bool CheckGlobalTransitions(BrainTransition[] globalTransitions)
        {
            foreach (var transition in globalTransitions)
            {
                bool result = transition.Decision.Decide(Owner);

                if (result)
                {
                    if (transition.TrueState == null) continue;
                    if (!Owner.GetEngine<IFsm>().IsCurrentState(transition.TrueState))
                    {
                        Owner.GetEngine<IFsm>().ChangeState(transition.TrueState);
                    }

                    return true;
                }
                else
                {
                    if (transition.FalseState == null) continue;
                    if (!Owner.GetEngine<IFsm>().IsCurrentState(transition.FalseState))
                    {
                        Owner.GetEngine<IFsm>().ChangeState(transition.FalseState);
                    }

                    return false;
                }
            }

            return false;
        }

        private void CheckLocalTransitions(IEnumerable<BrainTransition> transitions)
        {
            foreach (var transition in transitions)
            {
                bool result = transition.Decision.Decide(Owner);
                if (result)
                {
                    if (transition.TrueState != null && !Owner.GetEngine<IFsm>().IsCurrentState(transition.TrueState))
                    {
                        Owner.GetEngine<IFsm>().ChangeState(transition.TrueState);
                    }
                }
                else
                {
                    if (transition.FalseState != null && !Owner.GetEngine<IFsm>().IsCurrentState(transition.FalseState))
                    {
                        Owner.GetEngine<IFsm>().ChangeState(transition.FalseState);
                    }
                }
            }
        }
    }
}
