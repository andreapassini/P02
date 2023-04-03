using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PushOnTouch: MonoBehaviour
    {
        private Collider _collider;
        private Rigidbody _rb;

        public float force = 5f;

        private void Awake()
        {
            _rb = transform.GetComponent<Rigidbody>();
            _collider = transform.GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out ITouchable ipushable))
            {
                ipushable.Touch(transform);
            }
        }
    }
}