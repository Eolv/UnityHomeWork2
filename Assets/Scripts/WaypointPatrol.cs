using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints;
    private bool _isOnPatrol = true;
    private NavMeshAgent _agent;
    private int _currentWaypointIndex = 0;

    public bool IsOnPatrol
    {
        get { return _isOnPatrol; }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        _agent.SetDestination(_waypoints[0].position);
    }

    void Update()
    {
        if (_isOnPatrol)
        {
            if (_agent.remainingDistance <= _agent.stoppingDistance)
            {                
                _currentWaypointIndex = ++_currentWaypointIndex % _waypoints.Length;                
                _agent.SetDestination(_waypoints[_currentWaypointIndex].position);                
            }
        }
    }

    public void StartPatrol()
    {
        _isOnPatrol = true;
        _agent.SetDestination(_waypoints[_currentWaypointIndex].position);
    }

    public void StopPatrol()
    {
        _isOnPatrol = false;
    }
}
