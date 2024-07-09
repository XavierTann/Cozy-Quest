using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Armor", menuName = "ArmorSO")]
public class ArmorSO : ItemSO, INonStackableItems
{
    [SerializeField]
    private float defence;

    [SerializeField]
    private float weight;

    [SerializeField]
    private string specialEffects;

    public float Defence
    {
        get => defence;
    }
    public float Weight
    {
        get => weight;
    }

    public string SpecialEffects
    {
        get => specialEffects;
    }
}
