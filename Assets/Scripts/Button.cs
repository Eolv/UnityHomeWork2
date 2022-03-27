using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWork2
{
    public class Button : MonoBehaviour, ITakeDamage
    {
        [SerializeField] private GameObject _interactableObject;        
        private float _lastPressedTime = 0;
        private float _pressDelay = 1.5f;

        private void OnTriggerStay(Collider other)
        {
            if (Input.GetKeyDown(KeyCode.E) && other.transform.gameObject.CompareTag("Player"))
            {
                ButtonPressed();
            }                
        }

        public void Hit(float damage)
        {
            ButtonPressed();            
        }

        private void ButtonPressed()
        {
            if (Time.time > _lastPressedTime + _pressDelay)
            {
                _lastPressedTime = Time.time;
                if (_interactableObject.TryGetComponent(out IInterractWithButton interract))
                    interract.DoOnButtonPressedActions();
            }
            
        }
    }
}

