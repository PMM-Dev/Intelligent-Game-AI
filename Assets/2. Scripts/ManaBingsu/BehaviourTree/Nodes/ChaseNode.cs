using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ManaBingsu
{
    public class ChaseNode : Node
    {
        private Transform _target;
        private NavMeshAgent _agent;

        public ChaseNode(Transform target, NavMeshAgent agent)
        {
            _target = target;
            _agent = agent;
        }

        public override NodeState Evaluate()
        {
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
