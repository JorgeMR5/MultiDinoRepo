using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager instance;
    public AudioManager Instance { get { return instance; } }

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip[] musicClips;
    [SerializeField] AudioClip[] sfxClips;

    private void Awake()
    {
        instance = this;
    }
    public void ChangeMusic(int musicToChange)
    {
        musicSource.clip = musicClips[musicToChange];
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfxSource.PlayOneShot(sfxClips[sfxToPlay]);
    }
}
