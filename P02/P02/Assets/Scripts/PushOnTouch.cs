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
            if (collision.TryGetComponent(out IPushable ipushable))
            {
                ipushable.Push(collision.transform.position - transform.position);
                //collision.transform.GetComponent<Rigidbody>().AddExplosionForce(100f, collision.transform.position, 5);
            }
        }
    }
}