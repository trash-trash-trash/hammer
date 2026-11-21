using System.Collections;
using UnityEngine;

public class HammerSwingState : HammerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        hammerBrain.hammerSwing.SlamDown();
        yield return new WaitForSeconds(hammerBrain.hammerSwing.slamDuration);
        hammerBrain.attack.Attack();
        hammerBrain.ChangeState(HammerState.Cooldown);
    }
}