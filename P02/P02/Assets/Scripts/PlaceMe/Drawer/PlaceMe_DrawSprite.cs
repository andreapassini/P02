using System.Collections;
using System.Collections.Generic;
using MyNamespace;
using UnityEngine;

namespace PlaceMe
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlaceMe_DrawSprite : MonoBehaviour
    {
        private float _alphaMultiplier;
        private Transform _platfromPositionParent;
        private int _indexName;

        private PlaceMe_StayOnPlatform2D _stayOnPlatform2D;

        [HideInInspector] public bool _addStayOnPlatform = true;

        public void AddPlatformPosition(int indexName, float alphaMultiplier)
        {
            _alphaMultiplier = alphaMultiplier;
            _indexName = indexName;

            // Create platform
            Transform pos = CreatePlatform();

            // Sprite Renderer
            DrawPlatform(pos);
        }
        private void DrawPlatform(Transform pos)
        {
            // Sprite in this go
            if (transform.TryGetComponent<SpriteRenderer>(out SpriteRenderer sr))
            {
                // Add SR
                pos.gameObject.AddComponent<SpriteRenderer>().sprite = sr.sprite;
                SpriteRenderer spriteRenderer = pos.GetComponent<SpriteRenderer>();
                Color color = sr.color;
                if (_alphaMultiplier > 1f || _alphaMultiplier < 0f)
                    _alphaMultiplier = .5f;

                color.a = color.a * _alphaMultiplier;
                spriteRenderer.color = color;

                // Add script for inPlay
                if (!pos.gameObject.TryGetComponent<PlaceMe_DeactivateOnPlay>(
                        out PlaceMe_DeactivateOnPlay deactivateOnPlay))
                {
                    pos.gameObject.AddComponent<PlaceMe_DeactivateOnPlay>();
                }
            }
        }
        private Transform CreatePlatform()
        {
            // Check if parent exist
            if (!_platfromPositionParent)
                _platfromPositionParent = CreateParent();

            // Save position
            Vector3 platPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

            // Save rotation
            Quaternion platRot = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z,
                transform.rotation.w);

            // Save Scale
            Vector3 plstScale = new Vector3(transform.localScale.y, transform.localScale.x, transform.localScale.z);

            // Create Platfrom
            GameObject plat = new GameObject();
            plat.name = name + "_Pos_" + (_indexName++);
            plat.transform.SetPositionAndRotation(platPos, platRot);
            plat.transform.localScale = plstScale;
            plat.transform.SetParent(_platfromPositionParent, true);

            // Check parent
            isParent(plat.transform);

            // Stay on Platform
            if (_addStayOnPlatform)
            {
                _stayOnPlatform2D ??= gameObject.AddComponent<PlaceMe_StayOnPlatform2D>();
            }

            return plat.transform;
        }
        private Transform CreateParent()
        {
            // Debug.Log("Creating parent");
            // Create Parent
            GameObject parent = new GameObject();
            parent.name = "ParentOfNextPosition_" + (transform.name);
            parent.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);

            return parent.transform;
        }
        public void SetDrawPlatformParent(Transform t)
        {
            _platfromPositionParent = t;
        }
        private void isParent(Transform p)
        {
            if (transform.childCount > 0)
            {
                //Debug.Log("kids");
                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);

                    PlaceMe_DrawSprite childPlaceMe_Draw;

                    if (child.gameObject.TryGetComponent<PlaceMe_DrawSprite>(out PlaceMe_DrawSprite drawMe))
                    {
                        childPlaceMe_Draw = drawMe;
                    }
                    else
                    {
                        child.gameObject.AddComponent<PlaceMe_DrawSprite>();
                        childPlaceMe_Draw = child.GetComponent<PlaceMe_DrawSprite>();
                    }

                    childPlaceMe_Draw._addStayOnPlatform = this._addStayOnPlatform;
                    childPlaceMe_Draw.SetDrawPlatformParent(p);
                    childPlaceMe_Draw.AddPlatformPosition(i, _alphaMultiplier);
                }
            }
        }
    }
}
