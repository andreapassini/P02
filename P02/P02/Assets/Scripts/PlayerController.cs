using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    #region C# Events Arrows

    public delegate void OnShootArrow(float amountOfArrows);
    public static event OnShootArrow onShootArrow;

    #endregion
    
    // Input action not working well so using grab interac
    //public InputAction fireAction; 

    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private GameObject weaponToActivate;
    [SerializeField] private Transform firePoint;
    public float shootingForce = 50f;

    [FormerlySerializedAs("_arrowPrefab")]
    [Tooltip("If empty, Resource Load")]
    [SerializeField] private GameObject arrowPrefab;
    private float _arrows = 0;

    private void OnEnable()
    {
        HealthPotion.onHealthPotionHit += AddArrows;
    }
    private void OnDisable()
    {
        HealthPotion.onHealthPotionHit -= AddArrows;
    }

    void Start()
    {
        // path: Assets/Resources/arrow.prefab
        arrowPrefab ??= Resources.Load<GameObject>($"arrow.prefab");
    }
    public void AddArrows(float quantity)
    {
        _arrows += quantity;
    }
    public void ShootArrow(InputAction.CallbackContext obj)
    {
        Debug.Log(nameof(ShootArrow));

        if (arrowPrefab == null)
        {
            Debug.LogError("Arrow prefab not loaded");
            return;
        }
        
        GameObject newArrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        if (newArrow.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(firePoint.forward.normalized * shootingForce, ForceMode.Impulse);
        }
    }
    public void ShootArrow()
    {
        Debug.Log(nameof(ShootArrow));

        if (_arrows <= 0)
        {
            Debug.Log("Out of arrows");
            return;
        }
        
        SoundManager.Instance.PlayClip(shootSfx);
        _arrows--;
        onShootArrow?.Invoke(_arrows);

        if (arrowPrefab == null)
        {
            Debug.LogError("Arrow prefab not loaded");
            return;
        }
        
        GameObject newArrow = Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
        if (newArrow.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(firePoint.forward.normalized * shootingForce, ForceMode.Impulse);
        }
        
        Destroy(newArrow, 3f);
    }
    public void AddWeapon(float arrows)
    {
        Debug.Log(nameof(AddWeapon));
        
        // Activate the new GrabInteractable
        weaponToActivate.SetActive(true);
        _arrows += arrows;
        onShootArrow?.Invoke(arrows);
    }
}
