using System.Collections.Generic;

namespace kwon770
{
    public class Inverter : Node
    {
        protected Node _node;

        public Inverter(Node node)
        {
            this._node = node;
        }

        public override NodeState Evaluate()
        {
            switch (_node.Evaluate())
            {
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    break;
                case NodeState.Success:
                    _nodeState = NodeState.Failure;
                    break;
                case NodeState.Failure:
                    _nodeState = NodeState.Success;
                    return _nodeState;
                default:
                    break;
            }
            return _nodeState;
        }
    }
}