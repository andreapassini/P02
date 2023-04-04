using UnityEngine;

namespace DefaultNamespace {
    public class HealthPotion: MonoBehaviour, IHittable
    {
        #region C# Event Health Potion Hit
            public delegate void OnHealthPotionHit(float gainedArrows);
            public static event OnHealthPotionHit onHealthPotionHit;
        #endregion

        [SerializeField] private GameObject vfxHealthExplosion;
        [SerializeField] private AudioClip sfxHealthExplosion;
        public float destroyEffectsDuration = 5f;
        
        private int _gainedArrows = 10;
        
        public void Hit(Transform hitter)
        {
            onHealthPotionHit?.Invoke(_gainedArrows);
            DestroyPotion(hitter);
        }

        private void DestroyPotion(Transform hitter)
        {
            // Destroy arrow
            if(hitter != null)
                Destroy(hitter);
            
            // Destroy vfx
            if (vfxHealthExplosion != null)
            {
                GameObject vfxInstantiated;
                vfxInstantiated = Instantiate(
                    vfxHealthExplosion,
                    transform.position,
                    transform.rotation
                );
                Destroy(vfxInstantiated, destroyEffectsDuration);
            }
            // Destroy sfx
            SoundManager.Instance.PlayClip(sfxHealthExplosion);
            
            // Destroy Potion
            Destroy(gameObject);
        }

        public static void InvokeArrowEvent(int numberOfArrows)
        {
            onHealthPotionHit?.Invoke(numberOfArrows);
        }
    }
}