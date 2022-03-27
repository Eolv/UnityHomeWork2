using UnityEngine;
using UnityEngine.UI;

namespace HomeWork2
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private Transform _turretTopTower;
        [SerializeField] private GameObject _bulletPrefab;        
        [SerializeField] private Transform _bulletSpawnPosition;
        [SerializeField] private float _bulletPushForce = 1000f;
        private Transform _playerAimPoint;
        private float _turretDamageMultiplier = 2;
        private Vector3 _directionToPlayer;
        private float _lastFiringTime;

        void Start()
        {
            _playerAimPoint = FindObjectOfType<Player>()._enemiesTargetPoint;
        }

        void FixedUpdate()
        {
            Vector3 vectorToTarget = _playerAimPoint.position - _turretTopTower.position;
            Vector3 correctionAngle = new Vector3(0, Mathf.Pow(vectorToTarget.magnitude/8, 3), 0);

            _directionToPlayer = _playerAimPoint.position - _turretTopTower.position + correctionAngle;
            if (_directionToPlayer.magnitude < 100)
            {
                _turretTopTower.rotation = Quaternion.RotateTowards(_turretTopTower.rotation, Quaternion.LookRotation(_directionToPlayer), 30 * Time.fixedDeltaTime);
                if (Vector3.Angle(_turretTopTower.forward, _playerAimPoint.position - _turretTopTower.transform.position) < 40)
                {
                    Fire();
                }               
            }
        }

        private void Fire()
        {
            if (Time.time - _lastFiringTime > 1)
            {
                _lastFiringTime = Time.time;
                //_isFire = false;
                var bulletObject = Instantiate(_bulletPrefab, _bulletSpawnPosition.position, _bulletSpawnPosition.rotation);
                var bullet = bulletObject.GetComponent<Bullet>();
                bullet.Init(_playerAimPoint, _bulletPushForce, _turretDamageMultiplier);
                //Invoke(nameof(Reloading), _cooldown);                
            }
        }

    }
}
