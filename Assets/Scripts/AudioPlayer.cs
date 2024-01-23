using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;

    public AudioSource musicSource, sfxSource;
    public Sounds[] musicThemes;

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

    //reproduce musica en bucle
    public void PlayMusic(string songName, float volume = 1f)
    {
        Sounds song = Array.Find(musicThemes, x => x.name == songName);
        if(song == null) { Debug.LogWarning($"Song {songName} not found"); }
        else
        {
            musicSource.clip = song.clip;
            musicSource.volume = volume;
            musicSource.Play();
        }
    }

    //reproduce un sfx una vez
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        sfxSource.volume = volume;
        sfxSource.PlayOneShot(clip);
    }

    //para todos los sonidos
    public void StopAllSounds()
    {
        musicSource.Stop();
        sfxSource.Stop();
    }
}

[System.Serializable]
public class Sounds
{
    public string name;
    public AudioClip clip;
}
