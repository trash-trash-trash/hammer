using System;
using System.Collections.Generic;
using UnityEngine;

public class ITalk : MonoBehaviour
{
    public GameObject interactionIndicator;

    private bool playerInRange;
    
    public List<string> dialogue = new List<string>();  

    private void Start()
    {
        interactionIndicator.SetActive(false);
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
            
            interactionIndicator.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            interactionIndicator.SetActive(false);
            
            PlayerTalk playerTalk = other.gameObject.GetComponentInParent<PlayerTalk>();
            if (playerTalk != null)
            {
                playerTalk.FlipCanTalkTo(false);
                playerTalk.speakingTo = null;
            }
        }
    }
}