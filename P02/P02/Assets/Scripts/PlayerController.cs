using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region C# Events Arrows

    public delegate void OnShootArrow(float amountOfArrows);

    public static event OnShootArrow onShootArrow;

    #endregion
    
    public InputAction fireAction;

    [SerializeField] private Transform firePoint;
    public float shootingForce = 50f;

    [Tooltip("If empty, Resource Load")]
    [SerializeField] private GameObject _arrowPrefab;
    private float _arrows = 0;

    private void OnEnable()
    {
        HealthPotion.onHealthPotionHit += AddArrows;
        fireAction.performed += ShootArrow;
    }

    private void OnDisable()
    {
        HealthPotion.onHealthPotionHit -= AddArrows;
        fireAction.performed -= ShootArrow;
    }

    void Start()
    {
        // path: Assets/Resources/arrow.prefab
        _arrowPrefab ??= Resources.Load<GameObject>($"arrow.prefab");

        //StartCoroutine(AutoShoot());
    }

    public void AddArrows(float quantity)
    {
        _arrows += quantity;
    }
    public void ShootArrow(InputAction.CallbackContext obj)
    {
        Debug.Log(nameof(ShootArrow));

        if (_arrowPrefab == null)
        {
            Debug.LogError("Arrow prefab not loaded");
            return;
        }
        
        GameObject newArrow = Instantiate(_arrowPrefab, firePoint.position, firePoint.rotation);
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

        _arrows--;
        onShootArrow?.Invoke(_arrows);

        if (_arrowPrefab == null)
        {
            Debug.LogError("Arrow prefab not loaded");
            return;
        }
        
        GameObject newArrow = Instantiate(_arrowPrefab, firePoint.position, firePoint.rotation);
        if (newArrow.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(firePoint.forward.normalized * shootingForce, ForceMode.Impulse);
        }
        
        Destroy(newArrow, 3f);
    }

    private IEnumerator AutoShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            
            ShootArrow();
        }
    }
}
