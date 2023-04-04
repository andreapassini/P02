using UnityEngine;

namespace DefaultNamespace {
    public class HealthPotion: MonoBehaviour, IHittable, IPotion
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
            DestroyPotion(hitter, vfxHealthExplosion, sfxHealthExplosion, destroyEffectsDuration);
        }
        
        public static void InvokeArrowEvent(int numberOfArrows)
        {
            onHealthPotionHit?.Invoke(numberOfArrows);
        }

        public void DestroyPotion(Transform hitter, GameObject vfx, AudioClip sfx, float fxDuration)
        {
            // Destroy arrow
            if(hitter != null)
                Destroy(hitter.gameObject);
            
            // Destroy vfx
            GameObject vfxInstantiated;
            vfxInstantiated = Instantiate(
                vfx,
                transform.position,
                transform.rotation
            );
            Destroy(vfxInstantiated, fxDuration);
            
            // Destroy sfx
            SoundManager.Instance.PlayClip(sfx);
            
            // Destroy Potion
            Destroy(gameObject);
        }
    }
}