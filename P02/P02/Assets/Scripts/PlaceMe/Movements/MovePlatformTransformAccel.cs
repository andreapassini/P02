using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace BrashCreations.PlaceMe.Scripts.Movements
{
    public class MovePlatformTransformAccel : MonoBehaviour, IMovePlatform
    {
        // Move towards but increase and decrease speed (from a min to a max)
        // based on the distance

        private List<Transform> _tripPositions;
        private LoopType _loopType;
        private float _startingDelay;
        private float _restTime;
        private float _speedMultiplier;
        
        private IEnumerator _startingCor;
        private IEnumerator _restingCor;
        private bool _move = false;
        private int _positionIndex = 0;
        private Vector3 _nextPosition;
        private int _increment = 1;

        #region Accel fields and properties

        private const float _PercDistance = .25f;
        private float _minDistance;     //Based on a % of the distance
        private float _minSpeed;
        private float _maxSpeed;
        private float _startingDistance;    // Distance at time 0
        private float _accel = .75f;
        #endregion
        
        public void StartMoving(List<Transform> tripPositions, LoopType loopType, float startingDelay, float restTime, float speed, float stoppingDistance)
        {
            _tripPositions = new List<Transform>();
            foreach (var pos in tripPositions)
            {
                _tripPositions.Add(pos);
            }
            _loopType = loopType;
            _startingDelay = startingDelay;
            _restTime = restTime;
            _maxSpeed = speed;
            _minSpeed = 0f;
            
            _startingCor = StaringDelay(_startingDelay);
            _restingCor = RestingDelay(_restTime);
            _move = false;
            
            StartCoroutine(_startingCor); 
        }

        private void FixedUpdate()
        {
            if(!_move)
                return;

            
            // 1. Calculate the proportion of the distance
            float distance = (_nextPosition - transform.position).magnitude;
            float distanceProp = (distance/_startingDistance);
            
            // 2. Increase or decrease the speed based on the proportion of the distance
            if (distanceProp >= 1 - _PercDistance) // If near the starting pos
            {
                _speedMultiplier = Mathf.Min(_speedMultiplier + _accel * Time.deltaTime, 1);;
            }
            else if (distanceProp <= _PercDistance)
            {
                _speedMultiplier = Mathf.Max(_speedMultiplier - _accel * Time.deltaTime, 0.05f);;
            }

            Debug.Log( " _speedMult= " + _speedMultiplier  );
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _maxSpeed * _speedMultiplier * Time.fixedDeltaTime);
            
            CheckReachedPosition();
        }

        public void StopMoving()
        {
            _move = false;
            StopCoroutine(_restingCor);
            _restingCor = RestingDelay(_restTime);
        }
        public void RestartMoving()
        {
            _move = true;
        }
        private IEnumerator StaringDelay(float startingDelay)
        {
            yield return new WaitForSeconds(startingDelay);
            _nextPosition = _tripPositions[_positionIndex].position;
            _startingDistance = (_nextPosition - transform.position).magnitude;
            _move = true;
        }
        private IEnumerator RestingDelay(float restTime)
        {
            _move = false;

            yield return new WaitForSeconds(restTime);

            _move = true;
        }
        private void CheckReachedPosition()
        {
            if ((_nextPosition - transform.position).sqrMagnitude > 0)
                return;

            _positionIndex += IndexIncrement(); // Handle LoopBack and Forward
            
            //Check no Loop
            if (_positionIndex == _tripPositions.Count - 1 && _loopType == LoopType.NoLooping)
            {
                Debug.Log("End of the Ride");
                _move = false;
                return;
            }

            _nextPosition = _tripPositions[_positionIndex].position;
            _startingDistance = (_nextPosition - transform.position).magnitude;
            _speedMultiplier = _minSpeed;
            StartCoroutine(_restingCor);
        }
        private int IndexIncrement()
        {
            switch (_loopType)
            {
                case LoopType.LoopBackwards:
                    if ((_positionIndex == _tripPositions.Count - 1) || _positionIndex == 0)
                    {
                        _increment *= -1;
                    }
                    break;
                case LoopType.LoopForward:
                    if (_positionIndex == _tripPositions.Count - 1)
                    {
                        return (_tripPositions.Count - 1) * -1;
                    }
                    break;
            }
            return _increment;
        } 
    }
}
