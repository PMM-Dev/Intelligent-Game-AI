using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaBingsu.AStar
{
    public class Node
    {
        private bool _walkable;
        public bool Walkable 
        { 
            get { return _walkable; } 
            set { _walkable = value; }
        }
        private Vector3 _worldPosition;
        public Vector3 WorldPosition 
        { 
            get { return _worldPosition; } 
            set { _worldPosition = value; } 
        }

        public Node(bool walkable, Vector3 worldPosition)
        {
            _walkable = walkable;
            _worldPosition = worldPosition;
        }
    }
}

