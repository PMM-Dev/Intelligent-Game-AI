using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField] 
        private float _currentHealth;
        public float CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, _startingHealth); }
        }

        private void Start()
        {
            _currentHealth = _startingHealth;
            _material = GetComponent<MeshRenderer>().material;
        }

        public void Update()
        {
            CurrentHealth += Time.deltaTime * _healthRestoreRate;
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

