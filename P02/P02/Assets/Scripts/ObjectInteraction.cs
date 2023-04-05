using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObjectInteraction : MonoBehaviour, ITouchable
{
    #region C# Events for UI
    public delegate void OnTouch();
    public static event OnTouch onTouch;
    public delegate void OnDeTouch();
    public static event OnDeTouch onDeTouch;
    #endregion

    [SerializeField] private PlayerController playerController;
    [Space]
    
    [FormerlySerializedAs("vfxLifetime")] public float vfxDuration = 3f;

    [SerializeField] private AudioClip sfxHit;
    [SerializeField] private GameObject vfxHit;

    private Rigidbody _rb;
    private Animator[] _childAnimators;
    private TextMeshProUGUI _textMeshProUGUI;
    private static readonly int TouchTrigger = Animator.StringToHash("touch");
    private static readonly int IdleTrigger = Animator.StringToHash("idle");

    private void Awake()
    {
        _rb = transform.GetComponent<Rigidbody>();
        _childAnimators = transform.GetComponentsInChildren<Animator>();
        _textMeshProUGUI = transform.GetComponentInChildren<TextMeshProUGUI>();
        sfxHit ??= Resources.Load<AudioClip>("sfxDefault.wav");
    }

    public void TouchHoverEnter()
    {
        Debug.Log(nameof(TouchHoverEnter));
        if (sfxHit != null)
        {
            SoundManager.Instance.PlayClip(sfxHit);
        }

        if (vfxHit != null)
        {
            GameObject vfx;
            vfx = Instantiate(vfxHit, transform.position, transform.rotation);
            Destroy(vfx, vfxDuration);
        }

        TouchAnimation();
        TouchTextOn();
    }
    public void TouchHoverExit()
    {
        Debug.Log(nameof(TouchHoverExit));
        
        IdleAnimation();
        TouchTextOff();
    }
    public void TouchSelectEnter()
    {
        Debug.Log(nameof(TouchSelectEnter));
        
        // Add arrows to the player
        if (playerController == null)
        {
            Debug.Log("Missing playerController reference");
        }
        else
        {
            playerController.AddWeapon(10);
        }
        
        // Start the Level
        GameManager.Instance.StartLevel();

        Destroy(gameObject);
    }

    public void Touch(Transform toucher)
    {
        // Display Text
        
    }

    private void TouchAnimation()
    {
        foreach (var anim in _childAnimators)
        {
            anim.SetTrigger(TouchTrigger);
        }
        
        Debug.Log(nameof(TouchAnimation) + " animators: " + _childAnimators.Length);
    }
    private void IdleAnimation()
    {
        foreach (var anim in _childAnimators)
        {
            anim.SetTrigger(IdleTrigger);
        }
        
        Debug.Log(nameof(IdleAnimation) + " animators: " + _childAnimators.Length);
    }
    private void TouchTextOn()
    {
        onTouch?.Invoke();
    }
    private void TouchTextOff()
    {
        onDeTouch?.Invoke();
    }
}
