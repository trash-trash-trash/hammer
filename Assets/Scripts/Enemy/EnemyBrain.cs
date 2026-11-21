using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public enum EnemyState
{
    Idle,
    WalkToPlayer,
    DrawnToCoin,
    SwingHammer,
    Death
}

public class EnemyBrain : MonoBehaviour, IHear
{
    public EnemyState currentState;

    public HammerBrain hammerBrain;

    public GameObject idleState;
    public GameObject walkToPlayerState;
    public GameObject drawnToCoinState;
    public GameObject swingHammerState;
    public GameObject deathState;

    private GameObject prevState;

    public Dictionary<EnemyState, GameObject> statesDict = new Dictionary<EnemyState, GameObject>();

    public NavMeshAgent navMeshAgent;

    public Vector3 soundDestination;

    public Health health;
    public Rigidbody rb;

    void Start()
    {
        statesDict.Add(EnemyState.Idle, idleState);
        statesDict.Add(EnemyState.WalkToPlayer, walkToPlayerState);
        statesDict.Add(EnemyState.DrawnToCoin, drawnToCoinState);
        statesDict.Add(EnemyState.SwingHammer, swingHammerState);
        statesDict.Add(EnemyState.Death, deathState);

        ChangeState(EnemyState.Idle);

        health.AnnounceIsAlive += EnemyDied;
    }

    public void ChangeState(EnemyState newState)
    {
        if (statesDict.TryGetValue(newState, out GameObject state))
        {
            if (prevState != null)
            {
                prevState.SetActive(false);
            }

            prevState = state;
            currentState = newState;
            state.gameObject.SetActive(true);
        }
    }

    public void HeardNoise(Vector3 soundPosition)
    {
        if (!health.isAlive)
            return;
        
        soundDestination = soundPosition;
        ChangeState(EnemyState.DrawnToCoin);
    }

    private void EnemyDied(bool isAlive)
    {
        if (!isAlive)
            ChangeState(EnemyState.Death);
    }

    void OnDisable()
    {
        health.AnnounceIsAlive -= EnemyDied;
    }
}