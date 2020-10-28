using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip circusMusic;
    public AudioClip ambientSounds;

    private AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = ambientSounds;
        source.Play();
    }

    public void PlayCircusMusic()
    {
        source.clip = circusMusic;
        source.volume = 0.1f;
        source.Play();
    }

    public void PlayAmbient()
    {
        source.clip = ambientSounds;
        source.volume = 0.3f;
        source.Play();
    }
}
