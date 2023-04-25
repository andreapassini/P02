using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlaceMe;


namespace BrashCreations.PlaceMe.Scripts.Movements
{
    public class MovePlatformTransformMovePlatformTowards: MonoBehaviour, IMovePlatform
    {
        
        private List<Transform> _tripPositions;
        private LoopType _loopType;
        private float _startingDelay;
        private float _restTime;
        private float _speed;
        
        private IEnumerator _startingCor;
        private IEnumerator _restingCor;
        private bool _move = false;
        private int _positionIndex = 0;
        private Vector3 _nextPosition;
        private int _increment = 1;
        
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
            _speed = speed;
            
            _startingCor = StaringDelay(_startingDelay);
            _restingCor = RestingDelay(_restTime);
            _move = false;
            StartCoroutine(_startingCor);
        }

        private void FixedUpdate()
        {
            if(!_move)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speed * Time.fixedDeltaTime);

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