using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace kwon770
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
        private Cover[] _availableCovers;

        private Material _material;
        private Transform _bestCoverSpot;
        private NavMeshAgent _agent;

        private Node _topNode;

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
            ConstructBehaviorTree();
            Debug.Log(Vector3.Distance(_playerTransform.position, transform.position));
        }

        private void ConstructBehaviorTree()
        {
            IsCoveredAvailableNode isCoveredAvailableNode = new IsCoveredAvailableNode(_availableCovers, _playerTransform, this);
            GoToCoverNode goToCoverNode = new GoToCoverNode(_agent, this);
            HealthNode healthNode = new HealthNode(this, _lowHealthThreshold);
            IsCoveredNode isCoveredNode = new IsCoveredNode(_playerTransform, transform);
            ChaseNode chaseNode = new ChaseNode(_playerTransform, _agent, this);
            RangeNode chasingRangeNode = new RangeNode(_chasingRange, _playerTransform, transform);
            RangeNode shootingRangeNode = new RangeNode(_shootingRange, _playerTransform, transform);
            ShootNode shootNode = new ShootNode(_agent, this, _playerTransform);

            Sequence chaseSequence = new Sequence(new List<Node> { chasingRangeNode, chaseNode });
            Sequence shootSequence = new Sequence(new List<Node> { shootingRangeNode, shootNode });

            Sequence goToCoverSequence = new Sequence(new List<Node> { isCoveredAvailableNode, goToCoverNode });
            Selector findCoverSelector = new Selector(new List<Node> { goToCoverSequence, chaseSequence });
            Selector tryToTakeCoverSelector = new Selector(new List<Node> { isCoveredNode, findCoverSelector });
            Sequence mainCoverSequence = new Sequence(new List<Node> { healthNode, tryToTakeCoverSelector });

            _topNode = new Selector(new List<Node> { mainCoverSequence, shootSequence, chaseSequence });
        }

        private void Update()
        {
            _topNode.Evaluate();
            if (_topNode.NodeState == NodeState.Failure)
            {
                SetColor(Color.red);
            }

            _currentHealth += Time.deltaTime * _healthRestoreRate;
        }

        private void OnMouseDown()
        {
            CurrentHealth -= 10f;
        }

        public void SetColor(Color color)
        {
            _material.color = color;
        }

        public void setBestCoverSpot(Transform bestCoverSpot)
        {
            this._bestCoverSpot = bestCoverSpot;
        }

        public Transform GetBestCoverSpot()
        {
            return _bestCoverSpot;
        }
    }
}
