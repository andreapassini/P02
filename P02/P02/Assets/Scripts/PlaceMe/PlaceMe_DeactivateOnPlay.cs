using UnityEngine;

namespace PlaceMe
{
    public class PlaceMe_DeactivateOnPlay : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private MeshRenderer _meshRenderer;
        private MeshFilter _meshFilter;

        private void Awake()
        {
            if (_spriteRenderer == null)
            {
                if (transform.TryGetComponent(out SpriteRenderer spriteRenderer))
                    _spriteRenderer = spriteRenderer;
            }

            if (_meshRenderer == null)
            {
                if (transform.TryGetComponent(out MeshRenderer meshRenderer))
                    _meshRenderer = meshRenderer;
            }
        }

        void Start()
        {
            if(_spriteRenderer != null)
                _spriteRenderer.enabled = false;

            if(_meshRenderer != null)
                _meshRenderer.enabled = false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, .5f);
        }
    }
}
