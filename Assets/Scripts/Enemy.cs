using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _waypointList = new List<Transform>();
    [SerializeField]
    private float _chaseDistance;
    [SerializeField]
    private Player _player;

    private BaseState _currentState;

    [HideInInspector]
    public PatrolState PatrolState = new PatrolState();
    [HideInInspector]
    public ChaseState ChaseState = new ChaseState();
    [HideInInspector]
    public RetreatState RetreatState = new RetreatState();
    [HideInInspector]
    public NavMeshAgent NavMeshAgent;

    public List<Transform> Waypoints => _waypointList;
    public float ChaseDistance => _chaseDistance;
    public Player Player => _player;

    private void Awake()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
        _currentState = PatrolState;
        _currentState.EnterState(this);
    }

    private void Start()
    {
        if (_player != null)
        {
            _player.OnPowerUpStart += StartRetreating;
            _player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState(this);
        }
    }

    public void SwitchState(BaseState state)
    {
        _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }
}
