using UnityEngine;

public class EnemyWalkToPlayerState : EnemyStateBase
{
    public Transform player;

    public float stoppingDistance = 2f;

    public virtual void OnEnable()
    {
        base.OnEnable();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null)
        {
            enemyBrain.ChangeState(EnemyState.Idle);
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= stoppingDistance)
        {
            enemyBrain.ChangeState(EnemyState.SwingHammer);
            return;
        }

        enemyBrain.navMeshAgent.SetDestination(player.position);
    }
}