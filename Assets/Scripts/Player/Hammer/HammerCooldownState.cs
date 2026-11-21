using System.Collections;
using UnityEngine;

public class HammerCooldownState  : HammerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        hammerBrain.hammerSwing.ReturnToIdle();
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(hammerBrain.attackCooldown);
        hammerBrain.ChangeState(HammerState.Idle);
    }
}