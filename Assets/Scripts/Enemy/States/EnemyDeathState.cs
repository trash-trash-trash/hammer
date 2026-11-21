using UnityEngine;

public class EnemyDeathState : EnemyStateBase
{
    public virtual void OnEnable()
    {
        base.OnEnable();

        //unequip hammer on death?
        //needs force applied
        // enemyBrain.hammerBrain.playerTransform = null;
        // enemyBrain.hammerBrain.rb.isKinematic = false;
        // enemyBrain.hammerBrain.rb.useGravity = true;

        enemyBrain.rb.isKinematic = false;
        enemyBrain.navMeshAgent.enabled = false;
    }
}