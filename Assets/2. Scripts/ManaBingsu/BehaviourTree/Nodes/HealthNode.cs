using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaBingsu.BT
{
    public class HealthNode : Node
    {
        private EnemyAI _ai;
        private float _threshold;

        public HealthNode(EnemyAI ai, float threshold)
        {
            _ai = ai;
            _threshold = threshold;
        }

        public override NodeState Evaluate()
        {
            return _ai.CurrentHealth <= _threshold ? NodeState.Success : NodeState.Failure;
        }
    }
}
