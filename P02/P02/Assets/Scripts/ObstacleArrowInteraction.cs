using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ObstacleArrowInteraction : MonoBehaviour, IHittable
{
    public void Hit(Transform hit)
    {
        Destroy(hit.gameObject);
    }
}
