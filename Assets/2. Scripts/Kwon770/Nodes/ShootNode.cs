using UnityEngine;
using UnityEngine.AI;

namespace kwon770
{
    public class ShootNode : Node
    {
        private NavMeshAgent _agent;
        private EnemyAI _ai;
        private Transform _target;

        private Vector3 _currentVelocity;
        private float _smoothDamp;

        public ShootNode(NavMeshAgent agent, EnemyAI ai, Transform target)
        {
            this._agent = agent;
            this._ai = ai;
            this._target = target;
            _smoothDamp = 1f;
        }

        public override NodeState Evaluate()
        {
            _agent.isStopped = true;
            _ai.SetColor(Color.green);
            Vector3 direction = _target.position - _ai.transform.position;
            Vector3 currentDirection = Vector3.SmoothDamp(_ai.transform.forward, direction, ref _currentVelocity, _smoothDamp);
            Quaternion rotation = Quaternion.LookRotation(currentDirection, Vector3.up);
            _ai.transform.rotation = rotation;
            return NodeState.Running;
        }
    }
}