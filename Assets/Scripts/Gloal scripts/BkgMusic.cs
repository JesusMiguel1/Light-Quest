using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BkgMusic : MonoBehaviour
{
    public AudioClip audioClip;
    private AudioSource audioSource;
    private bool isPlaying;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void OnEnable()
    {
        audioSource.loop = true;
        audioSource = GetComponent<AudioSource>();
        
            if (audioSource != null && audioClip != null && isPlaying)
            {
                audioSource.volume = 1f;
               
                audioSource.PlayOneShot(audioClip);
            }
        

    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
