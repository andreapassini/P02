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
        
        [FormerlySerializedAs("tripPositions")]
        [Space]
        [SerializeField] private List<Transform> _positions;

        private int _positionIndex = 1;
        private Vector3 _nextPosition;
        [Tooltip("Used only with Lerp")]
        [SerializeField] private int _interpolationFramesCount = 50;
        [Tooltip("Used only with MoveTowards")]
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float stoppingDistance = .05f;
        private int _elapsedFrames = 0;

        private bool _move = false;
        public bool loop = false;

        [Space]
        private StayOnPlatform _stayOnPlatform;

        [SerializeField] private bool addStayOnPlatform = true;

        void Start()
        {
            _move = true;
            _nextPosition = _positions[0].position;
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
            
            if(_positions.Count <= _positionIndex && !loop)
                return;

            if (_positions.Count <= _positionIndex && loop)
                _positionIndex = 0;
            
            Vector3 nextPos = _positions[_positionIndex].position;
            _positionIndex++;

            _nextPosition = nextPos;
        }

        public void AddPlatformPosition()
        {
            // Check Empty Elemnts in list
            RemoveEmptyElements();

            // Add to List
            _positions.Add(CreatePlatform());

            DrawPlatform(_positions[_positions.Count - 1]);
        }
        
        private void RemoveEmptyElements()
        {
            if (_positions == null)
            {
                _positions = new List<Transform>();
                return;
            }

            int size = _positions.Count;
            for (int i = 0; i < size; i++)
            {
                var t = _positions[i];
                if (t.Equals(null))
                {
                    _positions.Remove(t);
                    size--;
                    i--;
                }
            }

            if (size == 0)
            {
                _positionIndex = 0;
            }
        }
        private Transform CreatePlatform()
        {
            // Check if parent exist
            if (!_platfromPositionParent)
                _platfromPositionParent = CreateParent();

            // Save position
            Vector3 platPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // Save rotation
            Quaternion platRot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z,
                transform.rotation.w);

            // Save Scale
            Vector3 plstScale = new Vector3(transform.localScale.y, transform.localScale.x, transform.localScale.z);

            // Create Platfrom
            GameObject plat = new GameObject();
            plat.name = name + "_Pos_" + (_positionIndex++);
            plat.transform.SetPositionAndRotation(platPos, platRot);
            plat.transform.localScale = plstScale;
            plat.transform.SetParent(_platfromPositionParent, true);

            // Check parent
            isParent(plat.transform);

            // Stay on Platform
            if (addStayOnPlatform)
            {
                _stayOnPlatform ??= gameObject.AddComponent<StayOnPlatform>();
            }

            return plat.transform;
        }
        private Transform CreateParent()
        {
            // Debug.Log("Creating parent");
            // Create Parent
            GameObject parent = new GameObject();
            parent.name = "ParentOfNextPosition_" + (transform.name);
            parent.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            return parent.transform;
        }
    }

    public enum MoveType
    {
        MoveTowards,
        Lerp,
    }
}
