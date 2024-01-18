using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioClip _inhaleSound;
    [SerializeField]private AudioClip _breathSound;
    [SerializeField]private AudioClip _explosionSound;

    private AudioSource _audioSource;

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayInhaleSound() => ChangeClipAndPlay(_inhaleSound);

    public void PlayBreathSound() => ChangeClipAndPlay(_breathSound);

    public void PlayExplosionSound() => ChangeClipAndPlay(_explosionSound);

    public void StopCurrentSound() => _audioSource.Stop();

    private void ChangeClipAndPlay(AudioClip audioClip)
    {
        StopCurrentSound();
        _audioSource.clip = audioClip;
        PlayCurrentSound();
    }
    private void PlayCurrentSound() => _audioSource.Play();
}
