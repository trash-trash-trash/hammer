using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    public Health health;

    public Slider slider;

    void Start()
    {
        slider.maxValue = health.maxHP;
        slider.value = 0;

        health.AnnounceCurrentHealth += SetHealth;
        
        health.Res();
    }

    private void SetHealth(int obj)
    {
        slider.value = obj;
    }

    private void OnDestroy()
    {
        health.AnnounceCurrentHealth -= SetHealth;
    }
}
