using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HomeWork2
{    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _bulletBaseDamage = 8;
        private float _bulletDamage = 8;
        private float _bulletLifeTime = 10f;
        private Rigidbody _bulletRigidbody;
        private bool _hadFirstImpact = false;
        private Transform _bulletTarget;

        private void Awake()
        {
            _bulletRigidbody = GetComponent<Rigidbody>();
        }

        public void Init(Transform target, float bulletPushForce, float damageMultiplier)
        {
            _bulletDamage = _bulletBaseDamage * damageMultiplier;
            _bulletTarget = target;            
            _bulletRigidbody.AddForce(transform.forward * bulletPushForce);
            Destroy(gameObject, _bulletLifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_hadFirstImpact)
            {
                _hadFirstImpact = true;
                if (collision.gameObject.TryGetComponent(out ITakeDamage target))
                    target.Hit(_bulletDamage);
            }

        }
    }
}
