using UnityEngine;

namespace DefaultNamespace
{
    public interface IHittable
    {
        void Hit(Transform hit);
    }
}