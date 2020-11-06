
namespace kwon770
{
    public class HealthNode : Node
    {
        private EnemyAI _ai;
        private float _threshold;

        public HealthNode(EnemyAI ai, float threshold)
        {
            this._ai = ai;
            this._threshold = threshold;
        }

        public override NodeState Evaluate()
        {
            return _ai.CurrentHealth <= _threshold ? NodeState.Success : NodeState.Failure;
        }
    }
}