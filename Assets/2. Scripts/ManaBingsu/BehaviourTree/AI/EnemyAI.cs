using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using ManaBingsu.BT;

namespace ManaBingsu
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] 
        private float _startingHealth;
        [SerializeField] 
        private float _lowHealthThreshold;
        [SerializeField] 
        private float _healthRestoreRate;

        [SerializeField]
        private float _chasingRange;
        [SerializeField]
        private float _shootingRange;


        [SerializeField]
        private Transform _playerTransform;
        [SerializeField]
        private Cover[] availableCovers;

        private Material _material;
        private Transform _bestCoverSpot;
        private NavMeshAgent _agent;

        private Node _topNode;

        [SerializeField] 
        private float _currentHealth;
        public float CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, _startingHealth); }
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _material = GetComponent<MeshRenderer>().material;

        }

        private void Start()
        {
            _currentHealth = _startingHealth;
            ConstructBehaviourTree();
        }

        private void ConstructBehaviourTree()
        {
            IsCoverAvailableNode coverAvailableNode = new IsCoverAvailableNode(availableCovers, _playerTransform, this);
            GoToCoverNode goToCoverNode = new GoToCoverNode(_agent, this);
            HealthNode healthNode = new HealthNode(this, _lowHealthThreshold);
            IsCoveredNode isCoveredNode = new IsCoveredNode(_playerTransform, transform);
            ChaseNode chaseNode = new ChaseNode(_playerTransform, _agent, this);
            RangeNode chasingRangeNode = new RangeNode(_chasingRange, _playerTransform, transform);
            RangeNode shootingRangeNode = new RangeNode(_shootingRange, _playerTransform, transform);
            ShootNode shootNode = new ShootNode(_agent, this);

            Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
            Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

            Sequence goToCoverSequence = new Sequence(new List<Node> { coverAvailableNode, goToCoverNode });
            Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
            Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
            Selector mainCoverSequence = new Selector(new List<Node> { healthNode, tryToTakeCoverSelector });

            _topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });
        }

        public void Update()
        {
            _topNode.Evaluate();
            if (_topNode.nodeState == NodeState.Failure)
            {
                SetColor(Color.red);
                _agent.isStopped = true;
            }

            CurrentHealth += Time.deltaTime * _healthRestoreRate;
        }

        private void OnMouseDown()
        {
            CurrentHealth -= 10f;
        }

        public void SetColor(Color color)
        {
            _material.color = color;
        }

        public void SetBestCoverSpot(Transform bestCoverSpot)
        {
            _bestCoverSpot = bestCoverSpot;
        }


        public Transform GetBestCoverSpot()
        {
            return _bestCoverSpot;
        }
    }
}

