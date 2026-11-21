using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum PlayerStates
{
    Idle,
    Walk,
    Swing,
    Talk
}

public class PlayerBrain : MonoBehaviour
{
   public PlayerStates currentState;

   public PlayerAttack playerAttack;
   public PlayerCoinToss playerCoinToss;
   public PlayerMovement playerMovement;

   public GameObject idleObj;
   public GameObject walkObj;
   public GameObject swingObj;
   public GameObject talkObj;
   private GameObject prevObj;
   
   public Dictionary<PlayerStates, GameObject> playerStatesDict = new Dictionary<PlayerStates, GameObject>();

   void Start()
   {
       playerStatesDict.Add(PlayerStates.Idle, idleObj);
       playerStatesDict.Add(PlayerStates.Walk, walkObj);
       playerStatesDict.Add(PlayerStates.Swing, swingObj);
       playerStatesDict.Add(PlayerStates.Talk, talkObj);
       
       ChangeState(PlayerStates.Idle);
   }

   public void ChangeState(PlayerStates newState)
   {
       if (playerStatesDict.TryGetValue(newState, out GameObject obj))
       {
           if(prevObj != null)
               prevObj.SetActive(false);
           
           obj.SetActive(true);
           prevObj = obj;
           currentState = newState;
       }
   }
}
