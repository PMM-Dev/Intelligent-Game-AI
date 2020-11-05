using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ManaBingsu.BT
{
    public class ChaseNode : Node
    {
        private Transform _target;
        private NavMeshAgent _agent;
        private EnemyAI _ai;

        public ChaseNode(Transform target, NavMeshAgent agent, EnemyAI ai)
        {
            _target = target;
            _agent = agent;
            _ai = ai;
        }

        public override NodeState Evaluate()
        {
            _ai.SetColor(Color.yellow);
            float distance = Vector3.Distance(_target.position, _agent.transform.position);
            if (distance > 0.2f)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target.position);
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
