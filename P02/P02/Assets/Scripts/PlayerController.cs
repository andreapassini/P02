using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
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
    }

    public void AddArrows(int quantity)
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
}
