using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StepSoundControl : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footSteps;
    private AudioSource _audioSource;
    
    
    private void Awake()
    {
     _audioSource = GetComponent<AudioSource>();   
    }

    public void Step()
    {
        var stepSound = _footSteps[Random.Range(0, _footSteps.Length)];
        _audioSource.clip = stepSound;
        _audioSource.PlayOneShot(stepSound);
    }

}
