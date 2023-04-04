using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIPointCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textPointName;
    [FormerlySerializedAs("textCounter")] [SerializeField] private TextMeshProUGUI textPointCounter;
    [Space] 
    
    [SerializeField] private TextMeshProUGUI textArrowName;
    [SerializeField] private TextMeshProUGUI textArrowCounter;
    [Space]
    
    private int _pointsCounter = 0;
    private float _arrowsCounter = 0;
    
    private void OnEnable()
    {
        SmokePotion.onSmokePotionHit += AddPoint;
        GameManager.onLevelStart += StartCounter;
        HealthPotion.onHealthPotionHit += AddArrows;
        PlayerController.onShootArrow += SetArrows;
    }
    private void OnDisable()
    {
        SmokePotion.onSmokePotionHit -= AddPoint;
        GameManager.onLevelStart -= StartCounter;
        HealthPotion.onHealthPotionHit -= AddArrows;
        PlayerController.onShootArrow -= SetArrows;
    }

    private void Start()
    {
        _pointsCounter = 0;
        _arrowsCounter = 0;
        textPointCounter.enabled = false;
        textPointName.enabled = false;
        
        textArrowName.enabled = false;
        textArrowCounter.enabled = false;
    }
    
    private void AddPoint(int pointToAdd)
    {
        _pointsCounter += pointToAdd;
        textPointCounter.text = _pointsCounter.ToString();
    }
    private void AddArrows(float arrows)
    {
        _arrowsCounter += arrows;
        textArrowCounter.text = _arrowsCounter.ToString();
    }
    private void SetArrows(float arrows)
    {
        _arrowsCounter = arrows;
        textArrowCounter.text = _arrowsCounter.ToString();
    }
    private void StartCounter()
    {
        textPointName.enabled = true;
        textPointCounter.enabled = true;
        textPointCounter.text = _pointsCounter.ToString();
        
        textArrowName.enabled = true;
        textArrowCounter.enabled = true;
        textArrowCounter.text = _arrowsCounter.ToString();
    }
}
