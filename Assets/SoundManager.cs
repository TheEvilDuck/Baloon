using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]Game _game;
    [SerializeField]AudioClip _explosionSound;
    [SerializeField]AudioClip _inhaleSound;
    [SerializeField]AudioClip _breathSound;

    private AudioSource _audioSource =>GetComponent<AudioSource>();

    private void OnEnable() 
    {
        _game.inhaleStarted+=OnInhaleStarted;
        _game.baloonCreated+=OnBaloonCreated;
        _game.playerBreathStarted+=OnBreathStarted;
        _game.playerBreathEnded+=OnBreathEnded;
    }
    private void OnDisable() 
    {
        _game.inhaleStarted-=OnInhaleStarted;
        _game.baloonCreated-=OnBaloonCreated;
        _game.playerBreathStarted-=OnBreathStarted;
        _game.playerBreathEnded-=OnBreathEnded;
    }
    private void Awake() {
        DontDestroyOnLoad(this);
    }
    private void OnInhaleStarted()
    {
        _audioSource.clip = _inhaleSound;
        _audioSource.Play();
    }
    private void OnBreathStarted()
    {
        _audioSource.Stop();
        _audioSource.clip = _breathSound;
        _audioSource.Play();
    }
    private void OnBreathEnded()
    {
        if (_audioSource.clip!=_explosionSound)
            _audioSource.Stop();
    }
    private void OnBaloonCreated()
    {
        _game.baloon.exploded+=OnBaloonExploded;
    }
    private void OnBaloonExploded()
    {
        _audioSource.Stop();
        _audioSource.clip = _explosionSound;
        _audioSource.Play();
    }

    
}
