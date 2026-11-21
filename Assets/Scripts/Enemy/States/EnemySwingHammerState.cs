using UnityEngine;
using System.Collections;

public class EnemyHammerState : EnemyStateBase
{
    private Coroutine attackCoroutine;

    public override void OnEnable()
    {
        base.OnEnable();

        attackCoroutine = StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        enemyBrain.hammerBrain.hammerSwing.PullBack();
        yield return new WaitForSeconds(enemyBrain.hammerBrain.hammerSwing.pullBackDuration);

        enemyBrain.hammerBrain.hammerSwing.SlamDown();
        
        yield return new WaitForSeconds(enemyBrain.hammerBrain.hammerSwing.slamDuration);
        enemyBrain.hammerBrain.attack.Attack();
        
        enemyBrain.hammerBrain.hammerSwing.ReturnToIdle();
        yield return new WaitForSeconds(enemyBrain.hammerBrain.hammerSwing.returnDuration);

        enemyBrain.ChangeState(EnemyState.WalkToPlayer);
    }

    private void OnDisable()
    {
        if (attackCoroutine != null)
            StopCoroutine(attackCoroutine);
    }
}