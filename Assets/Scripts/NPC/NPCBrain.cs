using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum NPCStates
{
    Idle,
    Talk
}

public class NPCBrain : MonoBehaviour
{
    public NPCStates currentState;

    public GameObject idleObj;
    public GameObject talkObj;
    private GameObject prevObj;
    
    public Dictionary<NPCStates, GameObject> statesDict = new Dictionary<NPCStates, GameObject>();

    void Start()
    {
        statesDict.Add(NPCStates.Idle, idleObj);
        statesDict.Add(NPCStates.Talk, talkObj);
        
        ChangeState(NPCStates.Idle);
    }

    public void ChangeState(NPCStates newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject obj))
        {
            if(prevObj != null)
                prevObj.SetActive(false);
            
            obj.SetActive(true);
            prevObj = obj;
            currentState = newState;
        }
    }
}
