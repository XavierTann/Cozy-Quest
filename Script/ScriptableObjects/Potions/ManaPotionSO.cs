using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Mana Potion", menuName = "ManaPotionSO")]
public class ManaPotionSO : PotionSO
{
    [SerializeField]
    private float manaGain;

    [SerializeField]
    private float duration;

    [SerializeField]
    private string specialEffects;

    public float ManaGain
    {
        get => manaGain;
    }
    public float Duration
    {
        get => duration;
    }

    public string SpecialEffects
    {
        get => specialEffects;
    }
}
