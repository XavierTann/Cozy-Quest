using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Health Potion", menuName = "HealthPotionSO")]
public class HealthPotionSO : PotionSO
{
    [SerializeField]
    private float healAmount;

    [SerializeField]
    private float healDuration;

    [SerializeField]
    private string specialEffects;

    public float HealAmount
    {
        get => healAmount;
    }
    public float HealDuration
    {
        get => healDuration;
    }

    public string SpecialEffects
    {
        get => specialEffects;
    }
}
