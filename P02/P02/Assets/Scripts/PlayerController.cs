using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    public float shootingForce = 50f;

    [Tooltip("If empty, Resource Load")]
    [SerializeField] private GameObject _arrowPrefab;
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
        _arrowPrefab ??= Resources.Load<GameObject>($"arrow.prefab");
    }

    public void AddArrows(int quantity)
    {
        _arrows += quantity;
    }
    public void ShootArrow()
    {
        GameObject newArrow = Instantiate(_arrowPrefab, firePoint.position, firePoint.rotation);
        if (newArrow.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.useGravity = true;
            rb.AddForce(firePoint.forward.normalized * shootingForce, ForceMode.Impulse);
        }
    }
}
