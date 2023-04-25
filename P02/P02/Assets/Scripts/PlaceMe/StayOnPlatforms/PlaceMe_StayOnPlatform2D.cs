using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyNamespace
{
    public class PlaceMe_StayOnPlatform2D : MonoBehaviour
    {
        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Stay on platform, changing the player parent
            collision.transform.SetParent(transform, false);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Leave platform, changing the player parent
            collision.transform.parent = null;
        }

    }
}
