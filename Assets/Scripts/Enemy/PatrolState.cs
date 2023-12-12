using UnityEngine;

public class PatrolState : BaseState
{
    private Vector3 _destinationPosition;
    private bool _isMoving;

    public void EnterState(Enemy enemy)
    {
        _isMoving = false;

        if (enemy == null) return;

        enemy.Animator.SetTrigger("PatrolState");
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) <= enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        if (!_isMoving)
        {
            int randomIndex = Random.Range(0, enemy.Waypoints.Count);
            _destinationPosition = enemy.Waypoints[randomIndex].position;
            enemy.NavMeshAgent.destination = _destinationPosition;
            _isMoving = true;
        }
        else
        {
            if (Vector3.Distance(enemy.transform.position, _destinationPosition) <= 0.1f)
            {
                _isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {
        
    }
}
