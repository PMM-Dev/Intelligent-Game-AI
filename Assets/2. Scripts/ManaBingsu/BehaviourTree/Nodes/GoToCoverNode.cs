using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

namespace ManaBingsu.BT
{
    public class GoToCoverNode : Node
    {
        private NavMeshAgent _agent;
        private EnemyAI _ai;

        public GoToCoverNode(NavMeshAgent agent, EnemyAI ai)
        {
            _agent = agent;
            _ai = ai;
        }

        public override NodeState Evaluate()
        {
            Transform coverSpot = _ai.GetBestCoverSpot();
            if (coverSpot == null)
            {
                return NodeState.Failure;
            }
            _ai.SetColor(Color.blue);
            float distance = Vector3.Distance(coverSpot.position, _agent.transform.position);
            if (distance > 0.5f)
            {
                _agent.isStopped = false;
                _agent.SetDestination(coverSpot.position);
                return NodeState.Running;
            }
            else
            {
                _agent.isStopped = true;
                return NodeState.Success;
            }
        }
    }
}
