using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip menuClickSFX;
    public AudioClip slashSFX;
    public AudioClip coinSFX;
    public AudioClip dashSFX;
    public AudioClip jumpSFX;
    public AudioClip hurtSFX;
    public AudioClip slimeDeathSFX;
    public AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMenuClickSound()
    {
        audioSource.PlayOneShot(menuClickSFX);
    }

    public void PlaySlashSound()
    {
        audioSource.PlayOneShot(slashSFX);
    }

    public void PlayCoinCollectSound()
    {
        audioSource.PlayOneShot(coinSFX);
    }

    public void PlayDashSound()
    {
        audioSource.PlayOneShot(dashSFX);
    }

    public void PlayJumpSound()
    {
        audioSource.PlayOneShot(jumpSFX);
    }

    public void PlayPlayerHurtSound()
    {
        audioSource.PlayOneShot(hurtSFX);
    }

    public void PlaySlimeDeathSound()
    {
        audioSource.PlayOneShot(slimeDeathSFX);
    }
}
