using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ObjectInteraction : MonoBehaviour, IPushable
{
    [FormerlySerializedAs("vfxLifetime")] public float vfxDuration = 3f;
    
    [SerializeField] private AudioClip sfxHit;
    [SerializeField] private GameObject vfxHit;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = transform.GetComponent<Rigidbody>();
        sfxHit ??= Resources.Load<AudioClip>("sfxDefault.wav");
    }

    public void Hit()
    {
        Debug.Log("Playing hit effects");
        if (sfxHit != null)
        {
            SoundManager.Instance.PlayClip(sfxHit);
        }

        if (vfxHit != null)
        {
            GameObject vfx;
            vfx = Instantiate(vfxHit, transform.position, transform.rotation);
            Destroy(vfx, vfxDuration);
        }
    }

    public void Push(Vector3 force)
    {
        _rb.AddForce(force, ForceMode.Impulse);
    }
}
