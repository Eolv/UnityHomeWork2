using UnityEngine;

namespace HomeWork2
{    public class Trap : MonoBehaviour
    {
        [SerializeField] private float _armCooldown = 4;
        [SerializeField] private bool _isMoving = true;
        private float _trapDamage = 20;
        private bool _isArmed;

        void Start()
        {
            InvokeRepeating(nameof(Arm), 1f, _armCooldown);
        }

        private void Arm()
        {
            if (_isMoving)
            {
                if (_isArmed)
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x, -1, transform.position.z);
                _isArmed = !_isArmed;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.TryGetComponent(out ITakeDamage target);
                target.Hit(_trapDamage * Time.deltaTime);
            }
        }
    }
}

