using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class SmokePotion: MonoBehaviour, IHittable, IPotion
    {
        #region C# events Smoke Potion
            public delegate void OnSmokePotionHit(int pointValue);
            public static event OnSmokePotionHit onSmokePotionHit;
        #endregion
        
        private int _points = 1;

        [SerializeField] private GameObject vfxExplosion;
        [SerializeField] private AudioClip sfxExplosion;
        public float destroyEffectsDuration = 5f;
        
        public void Hit(Transform hitTransform)
        {
            onSmokePotionHit?.Invoke(_points);
            DestroyPotion(hitTransform, vfxExplosion, sfxExplosion, destroyEffectsDuration);
        }
        
        public void DestroyPotion(Transform hitter, GameObject vfx, AudioClip sfx, float fxDuration)
        {
            // Destroy arrow
            if(hitter != null)
                Destroy(hitter);
            
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