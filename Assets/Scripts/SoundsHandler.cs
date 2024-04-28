using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsHandler : MonoBehaviour
{
    public static SoundsHandler instance;

    public AudioSource cardsSFXSource;
    public AudioSource gameOverSource;

    // all sound clips
    public AudioClip cardFlipClip;
    public AudioClip cardMatchClip;
    public AudioClip cardMisMatchClip;
    public AudioClip gameOverClip;

    private void Awake()
    {
        instance = this;
    }
    public void PlayCardFlipSFX()
    {
        cardsSFXSource.Stop();
        cardsSFXSource.clip = cardFlipClip;
        cardsSFXSource.Play();
    }
    public void PlayCardMatchSFX()
    {
        cardsSFXSource.Stop();
        cardsSFXSource.clip = cardMatchClip;
        cardsSFXSource.Play();
    }
    public void PlayCardMisMatchSFX()
    {
        cardsSFXSource.Stop();
        cardsSFXSource.clip = cardMisMatchClip;
        cardsSFXSource.Play();
    }
    public void PlayGameOver()
    {
        cardsSFXSource.Stop();
        gameOverSource.clip = gameOverClip;
        gameOverSource.Play();
    }
}
