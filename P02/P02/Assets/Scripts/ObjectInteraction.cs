using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public float objectSpawnedLifetime = 3f;
    [SerializeField] private GameObject prefabToSpawn;

    public void ActivateStart()
    {
        Debug.Log("ActivateStart");
        GameObject goSpawned;
        goSpawned = Instantiate(prefabToSpawn, Vector3.zero, Quaternion.identity);
        Destroy(goSpawned, objectSpawnedLifetime);
    }
}
