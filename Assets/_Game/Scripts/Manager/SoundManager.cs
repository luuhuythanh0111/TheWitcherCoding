using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource music2AudioSource;
    [SerializeField] private AudioSource btnClickSource;

    [SerializeField] private AudioClip btnClickClip;

    [SerializeField] private AudioSource[] audioSources;

    [Header("Audio Clip")]
    [SerializeField] private AudioClip[] audioClips;

    private int indexOfList = 0;

    public void PlayEffectSound(int index)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].isPlaying == true)
            {
                continue;
            }
            else
            {
                audioSources[i].PlayOneShot(audioClips[index]);
                break;
            }
        }
    }

    public bool GetMusicAudioSourceMute()
    {
        return musicAudioSource.mute;
    }

    public void MuteMusicAudioSource()
    {
        musicAudioSource.mute = true;
    }

    public void UnmuteMusicAudioSource()
    {
        musicAudioSource.mute = false;
    }

    public void ChangeMusicBackGround(bool inGame)
    {
        if(inGame == false)
        {
            musicAudioSource.mute = false;
            music2AudioSource.mute = true;
        }
        else
        {
            musicAudioSource.mute = true;
            music2AudioSource.mute = false;
        }
    }

    public void PLayeCLickSound()
    {
        btnClickSource.PlayOneShot(btnClickClip);
    }
}

public enum AudioClipEnum
{
    Throw = 0,
    Die = 1,
    GetHit = 2,
    ButtonClick = 3,
    Lose = 4,
    Win = 5,
    ScaleUp = 6,
}
