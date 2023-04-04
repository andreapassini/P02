using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class GameManager : MonoBehaviour
{
    #region C# event Start Level

    public delegate void OnLevelStart();
    public static event OnLevelStart onLevelStart;

    #endregion

    [SerializeField] private LauncherController launcherController;
    [SerializeField] public int levelOfDifficulty = 3;
    
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

        if (launcherController == null)
        {
            launcherController = FindObjectOfType<LauncherController>();
        }
    }

    public void StartLevel()
    {
        // Point text
        onLevelStart?.Invoke();
        launcherController.ActivateLaunchers(levelOfDifficulty);
    }

    public void OutOfArrows()
    {
        // END the level
        Debug.Log(nameof(OutOfArrows));
    }
}
