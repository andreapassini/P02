using System;
using UnityEngine;

namespace DefaultNamespace {
    public class Arrow: MonoBehaviour
    {
        [SerializeField] private AudioClip arrowHit;
        private void OnCollisionEnter(Collision collision)
        {
            if(collision.transform.TryGetComponent(out IHittable iHittable))
                iHittable.Hit(transform);

            //SoundManager.Instance.PlayClip(arrowHit);
            //Destroy(gameObject);
        }
    }
}