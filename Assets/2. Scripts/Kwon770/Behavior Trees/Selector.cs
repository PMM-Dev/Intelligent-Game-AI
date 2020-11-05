using System.Collections.Generic;

namespace kwon770
{
    public class Selector : Node
    {
        protected List<Node> _nodes = new List<Node>();

        public Selector(List<Node> nodes)
        {
            this._nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            foreach (var node in _nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Running:
                        _nodeState = NodeState.Running;
                        return _nodeState;
                    case NodeState.Success:
                        _nodeState = NodeState.Success;
                        return _nodeState;
                    case NodeState.Failure:
                        break;
                    default:
                        break;
                }
            }
            _nodeState = NodeState.Failure;
            return _nodeState;
        }
    }
}