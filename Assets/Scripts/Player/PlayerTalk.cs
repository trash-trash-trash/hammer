using System;
using UnityEngine;

public class PlayerTalk : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public PlayerBrain playerBrain;
    public PlayerInputs playerInputs;

    public bool canITalk = false;
    public bool isTalking = false;

    public ITalk speakingTo;

    private void Start()
    {
        playerInputs.AnnounceE += EPressed;
        dialogueManager = DialogueManager.Instance;
    }

    public void FlipCanTalkTo(bool input)
    {
        canITalk = input;
    }

    private void EPressed(bool input)
    {
        if (!input)
            return;

        if (isTalking)
        {
            dialogueManager.AdvanceDialogue();
            return;
        }

        if (!canITalk)
            return;

        if (speakingTo != null)
        {
            speakingTo.StartTalking();
            isTalking = true;
            playerBrain.ChangeState(PlayerStates.Talk);
            dialogueManager.StartConversation(speakingTo.dialogue);
            dialogueManager.AnnounceDialogueDone += StopTalking;
        }
    }

    public void StopTalking()
    {
        speakingTo.StopTalking();
        isTalking = false;
        playerBrain.ChangeState(PlayerStates.Idle);
        dialogueManager.AnnounceDialogueDone -= StopTalking;
        speakingTo = null;
    }

    private void OnDestroy()
    {
        playerInputs.AnnounceE -= EPressed;
    }
}