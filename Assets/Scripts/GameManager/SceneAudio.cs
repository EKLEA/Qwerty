using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SceneAudio : MonoBehaviour
{
    protected AudioSource audioSource => GetComponent<AudioSource>();
    public AudioClip sceneSound;
    void Start()
    {
        audioSource.clip = sceneSound;
        PlaySound();
    }

    void PlaySound()
    {
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlaySound();
        }
    }
}
