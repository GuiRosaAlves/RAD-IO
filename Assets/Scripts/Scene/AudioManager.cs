using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public AudioSource victoryAS;

    private void Awake()
    {
        instance = this;
        victoryAS = AddAudioSource();
    }

    private void OnDestroy()
    {
        if(instance != null)
        {
            instance = null;
        }
    }

    public AudioSource AddAudioSource()
    {
        return gameObject.AddComponent<AudioSource>();
    }

    public void PlayClip(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }
    public void PlayClip(AudioSource source, AudioClip clip, ulong delay)
    {
        source.clip = clip;
        source.Play(delay);
    }
}
