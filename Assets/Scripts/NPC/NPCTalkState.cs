using UnityEngine;

public class NPCTalkState : NPCStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        brain.iTalk.FlipTextBoxOnOff(false);
    }

    void OnDisable()
    {
        brain.iTalk.FlipTextBoxOnOff(true);
    }
}
