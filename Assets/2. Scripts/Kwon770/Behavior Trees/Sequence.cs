using System.Collections.Generic;

namespace kwon770
{
    public class Sequence : Node
    {
        protected List<Node> _nodes = new List<Node>();

        public Sequence(List<Node> nodes)
        {
            this._nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            bool isAnyNodeRunning = false;
            foreach (var node in _nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Running:
                        isAnyNodeRunning = true;
                        break;
                    case NodeState.Success:
                        break;
                    case NodeState.Failure:
                        _nodeState = NodeState.Failure;
                        return _nodeState;
                    default:
                        break;
                }
            }
            _nodeState = isAnyNodeRunning ? NodeState.Running : NodeState.Success;
            return _nodeState;
        }
    }
}