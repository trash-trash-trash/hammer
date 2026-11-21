using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PlayerInputs playerInputs;

    public HammerBrain hammerBrain;

    public bool canAttack = true;

    public void FlipCanAttack(bool input)
    {
        canAttack = input;
    }

    void Awake()
    {
        playerInputs.AnnounceRightClick += HandleRightClick;
    }

    private void HandleRightClick(bool input)
    {
        if (!canAttack)
            return;
        
        if (input)
            hammerBrain.ChargeAttack();
        else if (!input)
            hammerBrain.ReleaseAttack();
    }

    void OnDestroy()
    {
        playerInputs.AnnounceRightClick -= HandleRightClick;
    }
}