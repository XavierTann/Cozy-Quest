using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Potion", menuName = "PotionSO")]
public class PotionSO : ScriptableObject, IStackableItems
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float healAmount;
    [SerializeField] private float healDuration;
    [SerializeField] private int cost;
    [SerializeField] private string specialEffects;

    public string Name { get => name; }
    public float HealAmount { get => healAmount; }
    public float HealDuration { get => healDuration; }
    public int Cost { get => cost; }
    public string SpecialEffects { get => specialEffects; }

    public Sprite Sprite {get => sprite;}




}