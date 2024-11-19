using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource SFXSource;

    [Header("------- Audio Clip -------")]
    public AudioClip woohoo;
    public AudioClip ouch;
    public AudioClip frackoff;
    public AudioClip powerpickup;


    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
