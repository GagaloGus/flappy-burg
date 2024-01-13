using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;

    public AudioSource musicSource, sfxSource;
    private void Awake()
    {
        if (!instance) //instance  != null 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        musicSource = transform.Find("musicSource").GetComponent<AudioSource>();
        sfxSource = transform.Find("sfxSource").GetComponent<AudioSource>();

        musicSource.loop = true;
    }

    public void PlayMusic(AudioClip clip, float volume = 1f)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }

    public void StopAllSounds()
    {
        musicSource.Stop();
        sfxSource.Stop();
    }
}
