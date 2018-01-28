using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager instance;

    public static AudioSource victoryAS;

    public AudioClip victorySound;

    private void Awake()
    {
        instance = this;
        victoryAS = AddAudioSource(gameObject);
    }

    private void OnDestroy()
    {
        if(instance != null)
        {
            instance = null;
        }
    }

    public static AudioSource AddAudioSource(GameObject gameObject)
    {
        return gameObject.AddComponent<AudioSource>();
    }

    public static void PlayMusic(AudioSource source, AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    public static void PlayClip(AudioSource source, AudioClip clip, ulong delay)
    {
        source.clip = clip;
        source.Play(delay);
    }
}
