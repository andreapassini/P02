using System;
using UnityEngine;

namespace DefaultNamespace {
    public class Arrow: MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            foreach (Collision col in collision)
            {
                if(col.transform.TryGetComponent(out IHittable iHittable))
                    iHittable.Hit(transform);
            }
        }
    }
}