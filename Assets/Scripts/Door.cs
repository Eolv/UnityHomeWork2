using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace HomeWork2
{    public class Door : MonoBehaviour, IInterractWithButton
    {
        private bool _isOpen = false;
        private Vector3 _startingTransform;
        private Vector3 _moveRange = new Vector3 (0, 3, 0);
        void Awake()
        {            
            _startingTransform = transform.position;            
        }

        public void DoOnButtonPressedActions()
        {            
            _isOpen = !_isOpen;            
            transform.position = _startingTransform + (_isOpen ? _moveRange : Vector3.zero);
        }

    }
}

