using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    public AudioClip gameBGM;
    public AudioClip bossBGM;
    public AudioClip gameOverBGM;

    public AudioClip bhBulletShootSFX;
    public AudioClip bhSFX;
    public AudioClip dBulletShootSFX;
    public AudioClip explosionSFX;
    public AudioClip rgbBulletShootSFX;

    public AudioClip enemyHitSFX;
    public AudioClip enemyDeadSFX;

    private void Awake()
    {
        instance = this;
    }

    public void PlayBGM(bool isPlayBGM)
    {
        if (!EnemySpawner.instance.isBossSpawned)
        {
            bgmAudioSource.clip = gameBGM;
        }
        else
        {
            bgmAudioSource.clip = bossBGM;
        }
        bgmAudioSource.volume = 1f;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();

        if (isPlayBGM)
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

    public void PlayGameOver()
    {
        bgmAudioSource.volume = 0.3f;
        bgmAudioSource.clip = gameOverBGM;
        bgmAudioSource.Play();
    }

    public void PlayBHBulletShootSFX()
    {
        sfxAudioSource.PlayOneShot(bhBulletShootSFX);
    }

    public void PlayBHSFX()
    {
        sfxAudioSource.PlayOneShot(bhSFX);
    }

    public void PlayDBulletShootSFX()
    {
        sfxAudioSource.PlayOneShot(dBulletShootSFX);
    }

    public void PlayExplosionSFX()
    {
        sfxAudioSource.PlayOneShot(explosionSFX);
    }

    public void PlayRGBBulletShootSFX()
    {
        sfxAudioSource.PlayOneShot(rgbBulletShootSFX);
    }

    public void PlayEnemyHitSFX()
    {
        sfxAudioSource.PlayOneShot(enemyHitSFX);
    }

    public void PlayEnemyDeadSFX()
    {
        sfxAudioSource.PlayOneShot(enemyDeadSFX);
    }
}