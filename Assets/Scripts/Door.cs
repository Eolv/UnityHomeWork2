using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace HomeWork2
{    public class Door : MonoBehaviour, IInterractWithButton
    {
        [SerializeField] private bool _autoOpen = false;
        [SerializeField] private Animator _animator;
        private readonly int isOpen = Animator.StringToHash("isOpen");        
        private Vector3 _startingTransform;
        private Vector3 _moveRange = new Vector3 (0, 3, 0);
        void Awake()
        {            
            _startingTransform = transform.position;            
        }

        public void DoOnButtonPressedActions()
        {
            OpenDoor();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IOpenDoors target) && _autoOpen)
                OpenDoor();
            Invoke(nameof(OpenDoor), 20);
        }


        private void OpenDoor()
        {            
            _animator.SetBool(isOpen, !_animator.GetBool(isOpen));
        }
    }
}

