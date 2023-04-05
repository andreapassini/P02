using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PotionLauncher : MonoBehaviour
{
    private Transform firePoint;

    public float minDelay, maxDelay;
    public float heathPotProbability = .25f;
    
    [Space]
    public float minForce = 10, maxForce = 20;

    [Space] 
    public GameObject smokePot;
    public GameObject healthPot;
    
    void Start()
    {
        firePoint = transform.Find("FirePoint");
        
        
        // Start Coroutine
        StartCoroutine(PotionLauncherCoroutine(minDelay, maxDelay, minForce,  maxForce, heathPotProbability));
    }

    private void LaunchPot(bool isHealthPot, float shootingForce)
    {
        GameObject newGO;
        // Instantiate pot
        switch (isHealthPot)
        {
            case true:
                newGO = healthPot;
            break;
            case false:
                newGO = smokePot;
            break;
        }
        
        GameObject newInstance;
        newInstance = Instantiate(newGO, firePoint.position, Random.rotation);
        Destroy(newInstance, 5f);

        // Add force
        newInstance.GetComponent<Rigidbody>().AddForce(Vector3.up * shootingForce, ForceMode.Impulse);
    }

    private IEnumerator PotionLauncherCoroutine(float minD, float maxD, float minF, float maxF, float healthPotProb)
    {
        float delay, force, healthProb;
        bool healthPot = false;
        while (true)
        {
            delay = Random.Range(minD, maxD);
            force = Random.Range(minF, maxF);
            healthProb = Random.Range(0f, 1f);
            //Debug.Log("healthProb " + healthProb + " - healthPotProb (Par) " + healthPotProb);
            if (healthProb <= healthPotProb)
            {
                healthPot = true;
            }
            else
            {
                healthPot = false;
            }
            
            yield return new WaitForSeconds(delay);

            LaunchPot(healthPot, force);
            healthPot = false;
        }
    }
}
