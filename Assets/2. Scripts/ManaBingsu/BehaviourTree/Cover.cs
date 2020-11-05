using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaBingsu.BT
{
    public class Cover : MonoBehaviour
    {
        [SerializeField]
        private Transform[] _coverSpots;

        public Transform[] CoverSpots
        {
            get => _coverSpots;
        }
    }
}

