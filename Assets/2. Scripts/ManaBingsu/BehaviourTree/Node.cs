using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

namespace ManaBingsu.BT
{
    [System.Serializable]
    public abstract class Node
    {
        protected NodeState _nodeState;
        public NodeState nodeState { get => _nodeState; }

        public abstract NodeState Evaluate();
    }

    public enum NodeState
    {
        Running,
        Success,
        Failure,
    }
}


