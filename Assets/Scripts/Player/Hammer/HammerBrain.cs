using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum HammerState
{
    Idle,
    PullBack,
    Swing,
    Cooldown
}

public class HammerBrain : MonoBehaviour
{
    public HammerAttack attack;
    
    [Header("Hammer Stats")] 
    public float attackTime = .5f;
    public float attackCooldown = 1.5f;
    
    public HammerState hammerState = HammerState.Idle;

    public GameObject idleObj;
    public GameObject pullBackObj;
    public GameObject swingObj;
    public GameObject cooldownObj;

    private GameObject prevStateObj;

    public Dictionary<HammerState, GameObject> hammerStatesDict = new Dictionary<HammerState, GameObject>();

    public HammerSwing hammerSwing;

    public Transform playerTransform;
    
    public Rigidbody rb;

    void Awake()
    {
        hammerStatesDict.Add(HammerState.Idle, idleObj);
        hammerStatesDict.Add(HammerState.PullBack, pullBackObj);
        hammerStatesDict.Add(HammerState.Swing, swingObj);
        hammerStatesDict.Add(HammerState.Cooldown, cooldownObj);
        
        ChangeState(HammerState.Idle);
    }

    void Update()
    {
        if (playerTransform == null)
            return;
        
        transform.position = playerTransform.position;
        transform.rotation = playerTransform.rotation * Quaternion.Euler(0f, 90f, 0f);
    }

    public void ChargeAttack()
    {
        if(hammerState == HammerState.Idle)
            ChangeState(HammerState.PullBack);
    }

    public void ReleaseAttack()
    {
        if(hammerState == HammerState.PullBack)
            ChangeState(HammerState.Swing);
    }

    public void ChangeState(HammerState newState)
    {
        if (!hammerStatesDict.TryGetValue(newState, out GameObject stateObj))
            return;

        if (prevStateObj != null)
            prevStateObj.SetActive(false);

        stateObj.SetActive(true);

        prevStateObj = stateObj;
        hammerState = newState;
    }
}