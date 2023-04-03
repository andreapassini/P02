using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class SmokePotion: MonoBehaviour, IHittable
    {
        public delegate void OnSmokePotionHit(int pointValue);
        public static event OnSmokePotionHit onSmokePotionHit;

        private Collider _collider;
        private int _points = 1;

        public void Hit(Transform hitTransform)
        {
            onSmokePotionHit?.Invoke(_points);
        }
    }
}