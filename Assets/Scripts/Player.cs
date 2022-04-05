using UnityEngine;
using UnityEngine.UI;
using HomeWork2;

namespace HomeWork2
{
    public class Player : MonoBehaviour, ITakeDamage, IHealable, IOpenDoors
    {
        [SerializeField] private GameObject _weaponArrowPrefab;
        [SerializeField] private GameObject _bombPrefab;
        public Transform _enemiesTargetPoint;
        [SerializeField] private Transform _weaponArrowSpawnPoint;
        [SerializeField] private Transform _bombSpawnPoint;
        [SerializeField] private Transform _cameraPoint;
        [SerializeField] private Text _GUITextPresenter;
        [SerializeField] private Animator _animator;
        [SerializeField] private Camera _camera;
        private readonly int _isWalking = Animator.StringToHash("isWalking");
        private float _playerRotationY;
        private float _playerForwardSpeed = 4;
        private float _playerStrafeSpeed = 4;
        private float _jumpForce = 400;
        private float _playerHealth = 100;
        private float _playerMaxHealth = 100;
        private float _bowFireDelay = 1.5f;
        private float _bowFireTime = -100f;
        private float _bombPushForce = 10000f;
        private float _bombLifeTime = 10f;
        private float _bombFireDelay = 10f;
        private float _bombFireTime = -100f;
        private float _jumpPresstimer = 0f;
        private bool _jumpButtonAlreadyPressed = false;
        private bool _fireOn = false;
        private bool _jumpOn = false;
        private bool _bombOn = false;
        private Rigidbody _playerRigidBody;
        private Collider _playerCollider;
        private Camera _mainCamera;
        private float _rotationSpeed = 120;


        void Start()
        {
            _playerCollider = GetComponent<Collider>();
            _playerRigidBody = GetComponent<Rigidbody>();
            _mainCamera = Camera.main;
        }

        void Update()
        {
            Debug.DrawRay(_mainCamera.transform.position, _mainCamera.ScreenPointToRay(Input.mousePosition).direction);

            if (Input.GetKey(KeyCode.Mouse0))
                _fireOn = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _jumpButtonAlreadyPressed = true;
                _jumpPresstimer = 0;
            }

            if (_jumpButtonAlreadyPressed)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    _jumpPresstimer += Time.deltaTime;
                }
                else
                {                    
                    _jumpButtonAlreadyPressed = false;
                    _jumpOn = true;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
                _bombOn = true;
        }

        private void FixedUpdate()
        {
            _playerRotationY = Input.GetAxis("Mouse X");
            transform.Rotate(0f, _playerRotationY * _rotationSpeed * Time.deltaTime, 0f, Space.World);
            MovePlayer();
            MoveCamera();
            ProcessHotKeys();
            _GUITextPresenter.text = $"HP {_playerHealth}";
        }

        #region ProcessHotKeys
        private void ProcessHotKeys()
        {
            if (_fireOn)
            {
                _fireOn = false;
                Fire();
            }

            if (_jumpOn)
            {
                _jumpOn = false;
                Jump();
            }

            if (_bombOn)
            {
                _bombOn = false;
                Bomb();
            }

        }
        #endregion

        #region Actions
        private void MovePlayer()
        {
            Vector3 _playerMoveDirection = new Vector3(Input.GetAxis("Horizontal") * _playerStrafeSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * _playerForwardSpeed * Time.deltaTime);
            _playerMoveDirection = transform.TransformDirection(_playerMoveDirection);
            _playerRigidBody.MovePosition(transform.position + _playerMoveDirection);


            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                _animator.SetBool(_isWalking, true);
            else
                _animator.SetBool(_isWalking, false);
        }

        private void MoveCamera()
        {
            float cameraSpeed = 3;
            float x = Mathf.Lerp(_camera.transform.position.x, _cameraPoint.transform.position.x, cameraSpeed * Time.deltaTime);
            float y = Mathf.Lerp(_camera.transform.position.y, _cameraPoint.transform.position.y, cameraSpeed * Time.deltaTime);
            float z = Mathf.Lerp(_camera.transform.position.z, _cameraPoint.transform.position.z, cameraSpeed * Time.deltaTime);
            Quaternion rotation = Quaternion.Slerp(_camera.transform.rotation, _cameraPoint.transform.rotation, cameraSpeed * Time.deltaTime);
            _camera.transform.position = new Vector3(x, y, z);
            _camera.transform.rotation = rotation;

        }

        private void Fire()
        {
            if (Time.time - _bowFireTime > _bowFireDelay)
            {
                _bowFireTime = Time.time;
                Instantiate(_weaponArrowPrefab, _weaponArrowSpawnPoint.position, _weaponArrowSpawnPoint.rotation);
            }
        }

        private void Bomb()
        {
            if (Time.time - _bombFireTime > _bombFireDelay)
            {
                _bombFireTime = Time.time;
                GameObject bombObject = Instantiate(_bombPrefab, _bombSpawnPoint.position, _bombSpawnPoint.rotation);
                Bomb bomb = bombObject.GetComponent<Bomb>();
                bomb.Init(_bombPushForce, _bombLifeTime);
            }
        }

        private void Jump()
        {
            if (IsGrounded())
                if (_jumpPresstimer > 1)
                {
                    _playerRigidBody.AddForce(Vector3.up * _jumpForce * 1.5f, ForceMode.Impulse);
                    _jumpPresstimer = 0;
                }
                else
                {
                    _playerRigidBody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
                }
        }
        #endregion

        private bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, _playerCollider.bounds.extents.y + 0.05f);
        }

        public void Hit(float damage)
        {
            if (_playerHealth > 0)
            {
                _playerHealth -= damage;
            }
            if (_playerHealth <= 0)
            {
                print("Вы проиграли!");
            }
        }

        public void Heal()
        {
            _playerHealth = _playerMaxHealth;
        }
    }
}

