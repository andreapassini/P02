using UnityEngine;

namespace DefaultNamespace {
    public class HealthPotion: MonoBehaviour, IHittable
    {
        public delegate void OnHealthPotionHit(int gainedArrows);
        public static event OnHealthPotionHit onHealthPotionHit;

        private int _gainedArrows = 10;
        
        public void Hit(Transform hit)
        {
            onHealthPotionHit?.Invoke(_gainedArrows);
        }
    }
}