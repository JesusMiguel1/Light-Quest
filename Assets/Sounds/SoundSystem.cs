using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundSystem
{
    public string name;
    public AudioClip audioClip;
    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 1f)]
    public float pitch;
    public float roll;
    public float pitchSpeed;
    public float rollSpeed;
    public float volumeSpeed;

    public bool loop;

    [HideInInspector]
    public AudioSource audioSource;

}
