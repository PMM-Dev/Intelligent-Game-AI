using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ManaBingsu.BT
{
    public class IsCoverAvailableNode : Node
    {
        private Cover[] _availableCovers;
        private Transform _target;
        private EnemyAI _ai;

        public IsCoverAvailableNode(Cover[] availableCovers, Transform target, EnemyAI ai)
        {
            _availableCovers = availableCovers;
            _target = target;
            _ai = ai;
        }

        public override NodeState Evaluate()
        {
            Transform bestCoverSpot = FindBestCoverSpot();
            _ai.SetBestCoverSpot(bestCoverSpot);
            return bestCoverSpot != null ? NodeState.Success : NodeState.Failure;
        }

        private Transform FindBestCoverSpot()
        {
            if (_ai.GetBestCoverSpot() != null)
            {
                if (CheckIfCoverIsValid(_ai.GetBestCoverSpot()))
                {
                    return _ai.GetBestCoverSpot();
                }
            }
            float minAngle = 90;
            Transform bestSpot = null;
            for (int i = 0; i < _availableCovers.Length; i++)
            {
                Transform bestSpotInCover = FindBestSpotInCover(_availableCovers[i], ref minAngle);
                if (bestSpotInCover != null)
                {
                    bestSpot = bestSpotInCover;
                }
            }
            return bestSpot;
        }

        private Transform FindBestSpotInCover(Cover cover, ref float minAngle)
        {
            Transform[] availableSpots = cover.CoverSpots;
            Transform bestSpot = null;

            for (int i = 0; i < availableSpots.Length; i++)
            {
                Vector3 direction = _target.position - availableSpots[i].position;
                if (CheckIfCoverIsValid(availableSpots[i]))
                {
                    float angle = Vector3.Angle(availableSpots[i].forward, direction);
                    if (angle < minAngle)
                    {
                        minAngle = angle;
                        bestSpot = availableSpots[i];
                    }
                }
            }
            return bestSpot;
        }

        private bool CheckIfCoverIsValid(Transform spot)
        {
            RaycastHit hit;
            Vector3 direction = _target.position - spot.position;
            if (Physics.Raycast(spot.position, direction, out hit))
            {
                if (hit.collider.transform != _target)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
