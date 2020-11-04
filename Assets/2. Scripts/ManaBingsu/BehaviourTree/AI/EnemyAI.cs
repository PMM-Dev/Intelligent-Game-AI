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
        private float _currentHealth;
        public float CurrentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, _startingHealth); }
        }

        private void Start()
        {
            _currentHealth = _startingHealth;
        }

        public void Update()
        {
            CurrentHealth += Time.deltaTime * _healthRestoreRate;
        }
    }
}

