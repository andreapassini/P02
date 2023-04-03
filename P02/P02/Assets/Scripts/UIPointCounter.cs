using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPointCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCounter;
    private int _pointsCounter = 0;
    
    private void OnEnable()
    {
        SmokePotion.onSmokePotionHit += AddPoint;
    }

    private void OnDisable()
    {
        SmokePotion.onSmokePotionHit -= AddPoint;
    }

    private void Start()
    {
        _pointsCounter = 0;
        textCounter.text = "";
    }

    private void AddPoint(int pointToAdd)
    {
        _pointsCounter += pointToAdd;
        textCounter.text = _pointsCounter.ToString();
    }
}
