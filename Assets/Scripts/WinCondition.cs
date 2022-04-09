using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour
{
    private bool _gameFinished = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_gameFinished)
            {
                _gameFinished = true;
                print("YOU HAVE COMPLETED THE GAME!!!");
                SceneManager.LoadScene(0);
            }
        }
    }
}
