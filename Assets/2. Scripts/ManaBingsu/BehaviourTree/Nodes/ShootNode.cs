using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ManaBingsu
{
    public class ShootNode : Node
    {
        private NavMeshAgent _agent;
        private EnemyAI _ai;

        public ShootNode(NavMeshAgent agent, EnemyAI ai)
        {
            _agent = agent;
            _ai = ai;
        }

        public override NodeState Evaluate()
        {
            _agent.isStopped = true;
            _ai.SetColor(Color.green);
            return NodeState.Running;
        }
    }
}
