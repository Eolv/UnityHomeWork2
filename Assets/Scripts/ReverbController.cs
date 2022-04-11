using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbController : MonoBehaviour
{
    [SerializeField] private AudioReverbZone _reverbZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            _reverbZone.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            _reverbZone.enabled = false;
    }

}
