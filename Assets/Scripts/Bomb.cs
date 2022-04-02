using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HomeWork2;

namespace HomeWork2
{
    public class Bomb : MonoBehaviour
    {
        //[SerializeField] private float _bombDamage = 10;
        private Collider _bombCollider;
        private Rigidbody _bombRigidbody;
        private float _bombDetonationTimer = 3f;
        private float _explosionRadius = 5;
        private float _explosionForce = 100000;        

        private void Awake()
        {
            _bombCollider = GetComponent<Collider>();
            _bombRigidbody = GetComponent<Rigidbody>();
        }

        public void Init(float bombPushforce, float bombLifeTime)
        {
            _bombRigidbody.AddForce(transform.forward * bombPushforce);                  
            Invoke(nameof(Detonate), _bombDetonationTimer);
        }

        public void Detonate()
        {
            Collider[] bombVictims = Physics.OverlapSphere(transform.position, _explosionRadius);
            foreach (Collider victim in bombVictims)
            {   
                print(victim.name);
                if (victim.TryGetComponent(out Rigidbody targetObject))
                {
                    Vector3 vectorToTarget = targetObject.position - transform.position;                    
                    targetObject.AddForce(vectorToTarget.normalized * Mathf.Lerp(0, _explosionForce, (_explosionRadius - vectorToTarget.magnitude)/_explosionRadius));                    
                }
            }
            Destroy(gameObject);            
        }
    }
}

