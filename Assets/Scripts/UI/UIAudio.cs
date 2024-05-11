using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{
    [SerializeField] AudioClip hover, click;
    AudioSource audioSource=> GetComponent<AudioSource>();
    public void SoundOnHover()
    {
        audioSource.PlayOneShot(hover);
    }
    public void SoundOnClick()
    {
        audioSource.PlayOneShot(click);
    }
}
