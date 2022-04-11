using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundContainer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _particles;


    public void PlaySound(AudioClip sound)
    {
        _particles.SetActive(true);
        _audioSource.PlayOneShot(sound);
        Destroy(gameObject, sound.length);
    }
}
