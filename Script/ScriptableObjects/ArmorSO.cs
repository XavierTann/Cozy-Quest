using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Armor", menuName = "ArmorSO")]
public class ArmorSO : ScriptableObject, IItems
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float defence;
    [SerializeField] private float weight;
    [SerializeField] private int cost;
    [SerializeField] private string specialEffects;

    public string Name { get => name;}
    public float Defence { get => defence; }
    public float Weight { get => weight; }
    public int Cost { get => cost; }
    public string SpecialEffects { get => specialEffects; }
}