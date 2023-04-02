using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBounceCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCounter;
    private int _counter = 0;
    
    private void OnEnable()
    {
        Bouncer.onWallBounce += AddBounce;
    }

    private void OnDisable()
    {
        Bouncer.onWallBounce -= AddBounce;
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
