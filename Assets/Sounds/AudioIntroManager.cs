using System;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioIntroManager : MonoBehaviour
{
    public SoundSystem[] sounds;
    void Awake()
    {
        foreach (SoundSystem sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.audioClip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.pitch = sound.pitch;
            sound.audioSource.loop = sound.loop;
        }
    }

    public void PlaySound(string soundName)
    {
        SoundSystem s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null) { return; }
        s.audioSource.Play();
    }
}
