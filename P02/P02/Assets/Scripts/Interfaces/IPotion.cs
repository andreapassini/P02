using UnityEngine;

namespace DefaultNamespace
{
    public interface IPotion
    {
        void DestroyPotion(Transform hitter, GameObject vfx, AudioClip sfx, float fxDuration);
    }
}