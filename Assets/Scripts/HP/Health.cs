using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int currentHP;
    public int maxHP;

    public bool isAlive = false;

    public event Action<int> AnnounceCurrentHealth;

    public event Action<bool> AnnounceIsAlive;

    void Awake()
    {
        Res();
    }

    public void Res()
    {
        ChangeHP(maxHP);
        isAlive = true;
    }

    public void ChangeHP(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
        
        if (currentHP <= 0)
        {
            currentHP = 0;
            isAlive = false;
        }
        
        AnnounceCurrentHealth?.Invoke(currentHP);
        AnnounceIsAlive?.Invoke(isAlive);
    }
}