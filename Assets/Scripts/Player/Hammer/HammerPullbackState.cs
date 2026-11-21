using UnityEngine;

public class HammerPullbackState : HammerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        hammerBrain.hammerSwing.PullBack();
    }
}