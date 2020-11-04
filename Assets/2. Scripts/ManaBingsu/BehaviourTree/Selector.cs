using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaBingsu
{
    public class Selector : Node
    {
        public List<Node> nodes = new List<Node>();

        public Selector(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                switch(nodes[i].Evaluate())
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
