using UnityEngine;
using UnityEngine.AI;

namespace kwon770
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
                return NodeState.Failure;

            _ai.SetColor(Color.yellow);
            float distance = Vector3.Distance(coverSpot.position, _agent.transform.position);
            if (distance > 0.2f)
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