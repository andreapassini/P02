using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region C# event Start Level

    public delegate void OnLevelStart();
    public static event OnLevelStart onLevelStart;

    #endregion
    
    public static GameManager Instance { get; private set; }
    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    public void StartLevel()
    {
        // Point text
        onLevelStart?.Invoke();
    }

    public void OutOfArrows()
    {
        // END the level
        Debug.Log(nameof(OutOfArrows));
    }
}
