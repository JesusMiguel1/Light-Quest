using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private static AudioManager instance;
    private AudioSource audioSource;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject soundManagerObject = new GameObject("SoundManager");
                instance = soundManagerObject.AddComponent<AudioManager>();
                instance.audioSource = soundManagerObject.AddComponent<AudioSource>();
            }
            return instance;
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.volume = 1f;
            audioSource.PlayOneShot(clip);
        }
    }

}
