using System;
using TMPro;
using UnityEngine;

namespace DefaultNamespace {
    public class UIPickUp: MonoBehaviour
    {
        private TextMeshProUGUI _textPickMeUp;

        private void OnEnable()
        {
            ObjectInteraction.onTouch += PickMeUpTextOn;
            ObjectInteraction.onDeTouch += PickMeUpTextOff;
        }
        private void OnDisable()
        {
            ObjectInteraction.onTouch -= PickMeUpTextOn;
            ObjectInteraction.onDeTouch -= PickMeUpTextOff;
        }
        private void Awake()
        {
            _textPickMeUp = transform.GetComponentInChildren<TextMeshProUGUI>();
            _textPickMeUp.enabled = false;
        }

        private void PickMeUpTextOn()
        {
            Debug.Log(nameof(PickMeUpTextOn));
            _textPickMeUp.enabled = true;
        }

        private void PickMeUpTextOff()
        {
            Debug.Log(nameof(PickMeUpTextOff));
            _textPickMeUp.enabled = false;
        }
    }
}