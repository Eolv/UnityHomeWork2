using UnityEngine;

namespace HomeWork2
{
    public class PlayerArrow : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidBody;
        private bool _hadFirstImpact = false;        
        private float _arrowPushSpeed = 150;
        private float _arrowDamage = 30;

        void Start()
        {
            Destroy(gameObject, 10f);
            _rigidBody.AddForce(transform.forward * _arrowPushSpeed);            
        }

        void FixedUpdate()
        {
            if (!_hadFirstImpact)
            transform.forward = Vector3.Slerp(transform.forward, _rigidBody.velocity.normalized, Time.deltaTime*4f);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _hadFirstImpact = true;
            if (collision.gameObject.TryGetComponent(out ITakeDamage takeDamage))
            {
                takeDamage.Hit(_arrowDamage);
            }
        }
    }
}

