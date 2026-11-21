using UnityEngine;

public class PlayerTalkState : PlayerStateBase
{
    public override void OnEnable()
    {
        base.OnEnable();
        playerBrain.playerAttack.FlipCanAttack(false);
        playerBrain.playerCoinToss.FlipCanToss(false);
        playerBrain.playerMovement.FlipCanMove(false);
    }

    void OnDisable()
    {
        playerBrain.playerAttack.FlipCanAttack(true);
        playerBrain.playerCoinToss.FlipCanToss(true);
        playerBrain.playerMovement.FlipCanMove(true);
    }
}
