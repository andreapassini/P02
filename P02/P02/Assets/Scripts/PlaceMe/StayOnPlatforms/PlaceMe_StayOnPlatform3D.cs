using UnityEngine;

namespace PlaceMe
{
    public class PlaceMe_StayOnPlatform3D: MonoBehaviour
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter(Collider collision)
        {
            // Stay on platform, changing the player parent
            collision.transform.SetParent(transform, false);
        }

        private void OnTriggerExit(Collider collision)
        {
            // Leave platform, changing the player parent
            collision.transform.parent = null;
        }
    }
}