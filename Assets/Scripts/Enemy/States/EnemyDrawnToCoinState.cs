using UnityEngine;

public class EnemyDrawnToCoinState : EnemyStateBase
{
   public virtual void OnEnable()
   {
      base.OnEnable();

      enemyBrain.navMeshAgent.SetDestination(enemyBrain.soundDestination);
   }
}
