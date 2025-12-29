using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip audioClip)
    {
        if(_audioSource.clip == null)
        {
            _audioSource.clip = audioClip;
            _audioSource.Play();
            return;
        }
        if(_audioSource.clip.name == audioClip.name && _audioSource.isPlaying)
            return;
        if(_audioSource.isPlaying)
            _audioSource.Stop();
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }
    
    public void TogglePauseMusic()
    {
        if(_audioSource.isPlaying)
            _audioSource.Pause();
        else
        {
            _audioSource.UnPause();
        }
    }
    
    public void StopMusic()
    {
        if(_audioSource.isPlaying)
            _audioSource.Stop();
    }
}
