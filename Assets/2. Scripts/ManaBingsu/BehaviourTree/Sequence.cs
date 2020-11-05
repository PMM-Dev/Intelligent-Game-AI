using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaBingsu.BT
{
    public class Sequence : Node
    {
        public List<Node> nodes = new List<Node>();

        public Sequence(List<Node> nodes)
        {
            this.nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            bool isAnyNodeRunning = false;
            for (int i = 0; i < nodes.Count; i++)
            {
                switch(nodes[i].Evaluate())
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
