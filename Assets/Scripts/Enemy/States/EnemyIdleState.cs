using System.Collections;
using UnityEngine;

public class EnemyIdleState : EnemyStateBase
{
    public virtual void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(HackWait());
    }

    private IEnumerator HackWait()
    {
        yield return new WaitForSeconds(0.01f);
        enemyBrain.ChangeState(EnemyState.WalkToPlayer);
    }
}