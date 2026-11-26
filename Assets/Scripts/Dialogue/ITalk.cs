using System;
using System.Collections.Generic;
using UnityEngine;

public class ITalk : MonoBehaviour
{
    public GameObject interactionIndicator;

    private bool playerInRange;

    public List<DialogueLine> dialogue = new List<DialogueLine>();

    public event Action<bool> AnnounceStartStopTalking;

    private void Start()
    {
        interactionIndicator.SetActive(false);
    }

    public void StartTalking()
    {
        AnnounceStartStopTalking?.Invoke(true);
    }

    public void StopTalking()
    {
        AnnounceStartStopTalking?.Invoke(false);
    }

    public void FlipTextBoxOnOff(bool input)
    {
        interactionIndicator.SetActive(input);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            PlayerTalk playerTalk = other.gameObject.GetComponentInParent<PlayerTalk>();
            if (playerTalk != null)
            {
                playerTalk.FlipCanTalkTo(true);
                playerTalk.speakingTo = this;
            }

            FlipTextBoxOnOff(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            FlipTextBoxOnOff(false);

            PlayerTalk playerTalk = other.gameObject.GetComponentInParent<PlayerTalk>();
            if (playerTalk != null)
            {
                playerTalk.FlipCanTalkTo(false);
                playerTalk.speakingTo = null;
            }
        }
    }
}