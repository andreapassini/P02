using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace MovingPlatformAndUI
{
    public class MovingPlatform : MonoBehaviour
    {
        [Tooltip("Move Towards: constant speed \n Lerp: mix of speeds")]
        public MoveType moveType = MoveType.MoveTowards;
        
        [Space]
        [SerializeField] private List<Transform> tripPositions;

        private int _pos = 1;
        private Vector3 _nextPosition;
        [Tooltip("Used only with Lerp")]
        [SerializeField] private int _interpolationFramesCount = 50;
        [Tooltip("Used only with MoveTowards")]
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float stoppingDistance = .05f;
        private int _elapsedFrames = 0;

        private bool _move = false;
        public bool loop = false;

        void Start()
        {
            _move = true;
            _nextPosition = tripPositions[0].position;
        }

        void Update()
        {
            CheckReachedPosition();
        }

        private void FixedUpdate()
        {
            if(!_move)
                return;

            switch (moveType)
            {
                case MoveType.MoveTowards:
                    MoveWithMoveTowards();
                    break;
                case MoveType.Lerp:
                    MoveWithLerp();
                    break;
            }
        }

        private void MoveWithLerp()
        {
            // float step = _speed * Time.deltaTime;
            // transform.position = Vector3.Lerp(transform.position, _nextPosition, step);
            
            float interpolationRatio = (float)_elapsedFrames / _interpolationFramesCount;
           
            transform.position = Vector3.Lerp(transform.position, _nextPosition, interpolationRatio);
            
            _elapsedFrames = (_elapsedFrames + 1) % (_interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
            
        }

        private void MoveWithMoveTowards()
        {
            transform.position = Vector3.MoveTowards(transform.position, _nextPosition, _speed);
        }

        private void CheckReachedPosition()
        {
            if ((_nextPosition - transform.position).magnitude > stoppingDistance)
                return;

            _elapsedFrames = 0;
            
            if(tripPositions.Count <= _pos && !loop)
                return;

            if (tripPositions.Count <= _pos && loop)
                _pos = 0;
            
            Vector3 nextPos = tripPositions[_pos].position;
            _pos++;

            _nextPosition = nextPos;
        }
    }

    public enum MoveType
    {
        MoveTowards,
        Lerp,
    }
}
