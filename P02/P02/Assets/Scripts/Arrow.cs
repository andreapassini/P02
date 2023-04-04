using System;
using UnityEngine;

namespace DefaultNamespace {
    public class Arrow: MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent(out IHittable iHittable))
                iHittable.Hit(transform);
            
            Destroy(gameObject);
        }
    }
}