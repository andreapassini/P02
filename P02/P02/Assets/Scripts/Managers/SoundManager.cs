using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    
    public static SoundManager Instance { get; private set; }
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

        if (_audioSource == null)
            _audioSource = transform.GetComponent<AudioSource>();
    }

    public void PlayClip(AudioClip clipToPlay)
    {
        _audioSource.clip = clipToPlay;
        _audioSource.Play();
    }
}
