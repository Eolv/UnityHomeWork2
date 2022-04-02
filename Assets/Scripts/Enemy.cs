using HomeWork2;
using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ITakeDamage
{
    [SerializeField] private GameObject _alarm;
    [SerializeField] private Collider _weaponHitZone; 
    [SerializeField] private WaypointPatrol _waypointPatrol;
    [SerializeField] private Transform _weaponPoint;
    [SerializeField] private TextMeshPro _textHitPoints;

    private Player _player;
    private Collider _playerCollider;
    private Vector3 _directionToPlayer;
    private NavMeshAgent _agent;
    private float _weaponDamage = 5;
    private float _spottingDistance = 15;
    private float _timeToLoseFromSight = 8f;
    private float _timeFromLastContact = 0;
    private float _swingTime = 0.4f;
    private bool _isLookingForPlayer = false;
    private bool _isChasingPlayer = false;
    private bool _sawPlayerBefore = false;
    private bool _readyToHit = true;
    private float _maxHitPoints = 100;
    private float _hitPoints;

    void Start()
    {
        _hitPoints = _maxHitPoints;
        _player = FindObjectOfType<Player>();
        _agent = GetComponent<NavMeshAgent>();
        _playerCollider = _player.gameObject.GetComponent<Collider>();
    }

    void Update()
    {

        _textHitPoints.text = _hitPoints.ToString();

        if (CheckPlayerInVision())
        {
            _sawPlayerBefore = true;

            if (_isLookingForPlayer)
            {
                _isLookingForPlayer = false;         
                print("Found you!");
            }
                
            if (!_isChasingPlayer)
            {
                print("I see you!");
                _isChasingPlayer = true;
                if (_waypointPatrol.IsOnPatrol)
                    _waypointPatrol.StopPatrol();               
                _alarm.SetActive(true);
            }
            
            _timeFromLastContact = 0;
        }
        else
        {
            if (_sawPlayerBefore)
            {
                print("Lost you fromg sight, trying to chase!");
                _sawPlayerBefore = false;
                _isLookingForPlayer = true; ;
            }
            _timeFromLastContact += Time.deltaTime;
        }

        if (_timeFromLastContact > _timeToLoseFromSight && _isLookingForPlayer)
        {
            _timeFromLastContact = 0f;
            _isChasingPlayer = false;
            _isLookingForPlayer = false;
            _alarm.SetActive(false);
            print("Could not find you, go back to patrol :(");
            _waypointPatrol.StartPatrol();
        }

        if (_isChasingPlayer)
            _agent.SetDestination(_player.transform.position);                

        if (_directionToPlayer.magnitude <= 2 && _readyToHit)
        {
            _readyToHit = false;
            StartCoroutine(WeaponHit());
        }

    }

    private bool CheckPlayerInVision()
    {
        _directionToPlayer = _player.transform.position - transform.position;
        if (_directionToPlayer.magnitude < _spottingDistance)
        {
            Ray ray = new Ray(transform.position, _directionToPlayer);
            Debug.DrawRay(transform.position, _directionToPlayer);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Player>(out Player player))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void Hit(float damage)
    {
        if (_hitPoints > 0)
        {
            _hitPoints -= damage;
        }
        if (_hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WeaponHit()
    {
        float cooldown = _swingTime;
        bool playerHit = false;
        while (cooldown > 0)
        {         
            cooldown -= Time.deltaTime;
            _weaponPoint.localEulerAngles = new Vector3(Mathf.PingPong(cooldown /_swingTime * 200, 100), 0, 0);
            if (_weaponHitZone.bounds.Intersects(_playerCollider.bounds) && !playerHit)
            {
                playerHit = true;
                _player.Hit(_weaponDamage);
            }
                
            yield return null;            
        }        
        _readyToHit = true;
    }
}
