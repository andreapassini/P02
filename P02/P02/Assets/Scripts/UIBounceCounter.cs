using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class UIBounceCounter : MonoBehaviour
{
    [SerializeField] private TextMeshPro textCounter;
    private int _counter = 0;
    
    private void OnEnable()
    {
        BounceCounter.onWallBounce += AddBounce;
    }

    private void OnDisable()
    {
        BounceCounter.onWallBounce -= AddBounce;
    }

    private void Start()
    {
        _counter = 0;
        textCounter.text = _counter.ToString();
    }

    private void AddBounce(Vector3 hitPosition)
    {
        _counter++;
        textCounter.text = _counter.ToString();
    }
}
