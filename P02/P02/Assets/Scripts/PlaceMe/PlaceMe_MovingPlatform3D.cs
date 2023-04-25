using System.Collections;
using System.Collections.Generic;
using BrashCreations.PlaceMe.Scripts.Movements;
using PlaceMe;
using UnityEngine;
using UnityEngine.Serialization;

namespace PlaceMe
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class PlaceMe_MovingPlatform3D : MonoBehaviour
    {
        public MovementType movementType = MovementType.TransformConstantSpeed;
        [Tooltip("Loop type: \n" +
                 " No Looping" + 
                 " Loop Backwards : After completing the run, loop back (from position n-1 to 0)" +
                 " Loop Forward: After completing the run, loop forward (from 0 to n-1)")]
        public LoopType loopType = LoopType.LoopForward;
        
        [Space]
        [Tooltip(
            "Sprite Renderer that will be attached to platform positions with chose alpha. If left empty a new parent will be get from this object")]
        [SerializeField]
        private PlaceMe_DrawMesh _meshRenderer;

        [Tooltip("Alpha Multiplier between 0 and 1. If left empty alpha will be 0.5f")] [SerializeField]
        private float _alphaMultiplier = .5f;

        [Tooltip("Speed of the platform")] [SerializeField]
        private float _speed = 3f;

        [Tooltip(
            "Parent object of every nex platform positions. If left empty a new parent will be created on the same level of this platform")]
        [SerializeField]
        private Transform _platfromPositionParent;

        [FormerlySerializedAs("restingTimeAtEachLocations")] [FormerlySerializedAs("restingPeriodLocations")] [Tooltip("Resting period of the platform at each location (default value = 0f)")]
        public float restingTimeAtEachLocation = 0f;
        
        [Tooltip("Starting position will be postion 0. Otherwise the position of the platform will be position 0")]
        [SerializeField]
        private bool _startAtPosition0 = false;

        [Tooltip("Start Moving on Unity.Start, otherwise you can call the function StartMovingPlatform()")]
        [SerializeField]
        private bool _startMovingOnStart = true;

        [Tooltip("WORKS ONLY WITH Lerp \n Regulate stopping distance, recommended around .02f - .03f")]
        private float _stoppingDistance = .025f;
        
        private PlaceMe_StayOnPlatform3D _stayOnPlatform3D;

        [Tooltip("Add Stay on Platform script. This allow player to stay on the platfrom when moving")]
        public bool addStayOnPlatform = true;

        private int _indexName = 0;

        [SerializeField] private float _startingDelay = 0f;
        private IEnumerator _moveCoroutine;
        
        [Space]
        [SerializeField] private List<Transform> _positions;
        private void Start()
        {
            RemoveEmptyElements();

            AddMyPosOnStart();

            // Start Moving Platform
            if (_positions == null || _positions.Count == 0)
                return;

            switch (movementType)
            {
                case MovementType.TransformConstantSpeed:
                    gameObject.AddComponent<MovePlatformTransformMovePlatformTowards>();
                    break;
                case MovementType.TransformLerp:
                    gameObject.AddComponent<MovePlatformTransformLerp>();
                    break;
            }
            
            transform.GetComponent<IMovePlatform>().StartMoving(
                _positions,
                loopType,
                _startingDelay,
                restingTimeAtEachLocation,
                _speed,
                _stoppingDistance
            );
        }

        public void AddPlatformPosition()
        {
            // Check Empty Elemnts in list
            RemoveEmptyElements();

            // Add to List
            _positions.Add(CreatePlatform());

            DrawPlatform(_positions[_positions.Count - 1]);
        }

        private void DrawPlatform(Transform pos)
        {
            // Mesh in this go
            if (transform.TryGetComponent<MeshFilter>(out MeshFilter mF)
                && transform.TryGetComponent<MeshRenderer>(out MeshRenderer mR))
            {
                Mesh mesh = mF.mesh;
                pos.gameObject.AddComponent<MeshFilter>().sharedMesh = mesh;
                pos.gameObject.AddComponent<MeshRenderer>().material = mF.GetComponent<MeshRenderer>().material;
                MeshFilter meshFilter = pos.GetComponent<MeshFilter>();
                MeshRenderer meshRenderer = pos.GetComponent<MeshRenderer>();
                Color color = mR.material .color;
                if (_alphaMultiplier > 1f || _alphaMultiplier < 0f)
                    _alphaMultiplier = .5f;

                color.a = color.a * _alphaMultiplier;
                meshRenderer.sharedMaterial.color = color;

                // Add script for inPlay
                if (!pos.gameObject.TryGetComponent<PlaceMe_DeactivateOnPlay>(
                        out PlaceMe_DeactivateOnPlay deactivateOnPlay))
                {
                    pos.gameObject.AddComponent<PlaceMe_DeactivateOnPlay>();
                }
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
            plat.name = name + "_Pos_" + (_indexName++);
            plat.transform.SetPositionAndRotation(platPos, platRot);
            plat.transform.localScale = plstScale;
            plat.transform.SetParent(_platfromPositionParent, true);

            // Check parent
            isParent(plat.transform);

            // Stay on Platform
            if (addStayOnPlatform)
            {
                if (!transform.TryGetComponent(out PlaceMe_StayOnPlatform3D stayOnPlatform))
                {
                    _stayOnPlatform3D ??= gameObject.AddComponent<PlaceMe_StayOnPlatform3D>();
                }
            }

            return plat.transform;
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
                _indexName = 0;
            }
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

        private void SetDrawPlatformParent(Transform t)
        {
            _platfromPositionParent = t;
        }

        private void isParent(Transform p)
        {
            if (transform.childCount > 0)
            {
                //Debug.Log("kids");
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);

                    PlaceMe_DrawMesh childPlaceMe_Draw;

                    if (child.gameObject.TryGetComponent<PlaceMe_DrawMesh>(out PlaceMe_DrawMesh drawMe))
                    {
                        childPlaceMe_Draw = drawMe;
                    }
                    else
                    {
                        child.gameObject.AddComponent<PlaceMe_DrawMesh>();
                        childPlaceMe_Draw = child.GetComponent<PlaceMe_DrawMesh>();
                    }

                    childPlaceMe_Draw._addStayOnPlatform = this.addStayOnPlatform;
                    childPlaceMe_Draw.SetDrawPlatformParent(p);
                    childPlaceMe_Draw.AddPlatformPosition(i, _alphaMultiplier);
                }
            }
        }
        
        private void AddMyPosOnStart()
        {
            AddPlatformPosition();

            List<Transform> list = ListHelper.CopyList(_positions);
            _positions.Clear();
            foreach (var t in list)
            {
                _positions.Add(t);
            }
        }

        private void OnDrawGizmos()
        {
            if (_positions == null)
            {
                _positions = new List<Transform>();
                return;
            }

            // Draw lines from this go to the other paltform position
            if (_positions.Count > 0 && _positions[0] != null)
            {
                RemoveEmptyElements();

                Gizmos.color = Color.white;

                int size = _positions.Count;
                for (int i = 1; i < size; i++)
                {
                    Gizmos.DrawLine(_positions[i - 1].transform.position, _positions[i].transform.position);

                    if (i == size - 1 && loopType == LoopType.LoopBackwards)
                    {
                        Gizmos.DrawLine(_positions[size - 1].transform.position, _positions[0].transform.position);
                    }
                }

                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, _positions[size - 1].transform.position);

                // Drawing to first target on top
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(transform.position, _positions[0].transform.position);
            }
        }

    }
}
