using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmAudioSource;

    public AudioClip gameBGM;

    public AudioClip bhBulletShootSFX;

    private void Awake()
    {
        instance = this;
    }

    public void PlayGameBGM(bool isPlayGameBGM)
    {
        if (bgmAudioSource.clip != gameBGM)
        {
            bgmAudioSource.clip = gameBGM;
            bgmAudioSource.loop = true;
            bgmAudioSource.Play();
        }

        if (isPlayGameBGM)
        {
            if (!bgmAudioSource.isPlaying)
            {
                bgmAudioSource.UnPause();
            }
        }
        else
        {
            if (bgmAudioSource.isPlaying)
            {
                bgmAudioSource.Pause();
            }
        }
    }
}
