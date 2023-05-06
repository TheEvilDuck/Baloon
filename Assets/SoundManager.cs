using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]Game _game;
    [SerializeField]AudioClip _explosionSound;

    private AudioSource _audioSource =>GetComponent<AudioSource>();

    private void OnEnable() {
        _game.baloonCreated+=OnBaloonCreated;
    }
    private void OnDisable() {
        _game.baloonCreated-=OnBaloonCreated;
    }

    private void OnBaloonCreated()
    {
        _game.baloon.exploded+=OnBaloonExploded;
    }
    private void OnBaloonExploded()
    {
        _audioSource.clip = _explosionSound;
        _audioSource?.Play();
    }
}
