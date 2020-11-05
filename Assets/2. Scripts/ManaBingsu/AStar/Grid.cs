using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaBingsu.AStar
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _unwalkableMask;
        public LayerMask UnwalkableMask
        {
            get { return _unwalkableMask; }
            set { _unwalkableMask = value; }
        }
        [SerializeField]
        private Vector2 _gridWorldSize;
        public Vector2 GridWorldSize
        {
            get { return _gridWorldSize; }
            set { _gridWorldSize = value; }
        }
        [SerializeField]
        private float _nodeRadius;
        public float NodeRadius
        {
            get { return _nodeRadius; }
            set { _nodeRadius = value; }
        }

        private Node[,] _grid;
        private float _nodeDiameter;
        private Point _gridSize;
        

        private void Start()
        {
            _nodeDiameter = _nodeRadius * 2;
            _gridSize.X = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
            _gridSize.Y = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);
            CreateGrid();
        }

        private void CreateGrid()
        {
            _grid = new Node[_gridSize.X, _gridSize.Y];
            Vector3 worldBottomLeft = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;

            for (int x = 0; x < _gridSize.X; x++)
            {
                for (int y = 0; y < _gridSize.Y; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * _nodeDiameter + +_nodeRadius) + Vector3.forward * (y * _nodeDiameter + NodeRadius);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));
        }
    }
}
