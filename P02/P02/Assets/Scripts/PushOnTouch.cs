using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PushOnTouch: MonoBehaviour
    {
        private Collider _collider;

        public float force = 5f;

        private void Start()
        {
            _collider = transform.GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out IPushable ipushable))
                ipushable.Push((other.transform.position - transform.position) * force);
        }
    }
}