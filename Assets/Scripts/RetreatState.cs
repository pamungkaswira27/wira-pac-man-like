using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log($"[{nameof(RetreatState)}]: Start Retreating");
    }

    public void UpdateState(Enemy enemy)
    {
        if (enemy.Player != null)
        {
            enemy.NavMeshAgent.destination = enemy.transform.position - enemy.Player.transform.position;
        }
    }

    public void ExitState(Enemy enemy)
    {
        Debug.Log($"[{nameof(RetreatState)}]: Stop Retreating");
    }
}