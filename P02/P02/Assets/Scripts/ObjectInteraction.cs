using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectInteraction : MonoBehaviour, IPushable
{
    public float vfxLifetime = 3f;
    
    [SerializeField] private AudioSource sfxHit;
    [SerializeField] private GameObject vfxHit;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = transform.GetComponent<Rigidbody>();
        string a = "Assets/Resources/sfxDefault.wav";
    }

    public void Hit()
    {
        Debug.Log("Playing hit effects");
        if(sfxHit != null)
            sfxHit.Play();
        
        if (vfxHit != null)
            return;
        
        GameObject vfx;
        vfx = Instantiate(vfxHit, transform.position, transform.rotation);
        Destroy(vfx, vfxLifetime);
    }

    public void Push(Vector3 force)
    {
        Debug.Log("Hit Sphere");
        _rb.AddForce(force, ForceMode.Impulse);
    }
}
