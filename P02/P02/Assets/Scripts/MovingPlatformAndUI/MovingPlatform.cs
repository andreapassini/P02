using System;
using System.Collections.Generic;
using UnityEngine;

namespace MovingPlatformAndUI
{
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private List<Transform> tripPositions;

        private int _pos = 0;
        private Vector3 _nextPosition;
        private readonly int _interpolationFramesCount = 50;
        private int _elapsedFrames = 0;

        private bool _move = false;
        private bool _loop = false;
        void Start()
        {
            _move = true;
        }

        void Update()
        {
            CheckReachedPosition();
        }

        private void FixedUpdate()
        {
            if(!_move)
                return;
            float interpolationRatio = (float)_elapsedFrames / _interpolationFramesCount;
            
            transform.position = Vector3.Lerp(transform.position, _nextPosition, interpolationRatio);
            
            _elapsedFrames = (_elapsedFrames + 1) % (_interpolationFramesCount + 1);  // reset elapsedFrames to zero after it reached (interpolationFramesCount + 1)
        }

        private void CheckReachedPosition()
        {
            if ((_nextPosition - transform.position).magnitude > 0)
                return;

            _elapsedFrames = 0;

            Vector3 nextPos = tripPositions[_pos].position;
            _pos++;

            _nextPosition = nextPos;
        }
    }
}
