using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Weapon", menuName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    [SerializeField] private new string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float speed;
    [SerializeField] private int cost;
    [SerializeField] private string specialEffects;

    public string Name { get => name; }
    public Sprite Sprite { get => sprite; }
    public float Damage { get => damage; }
    public float Range { get => range; }
    public float Speed { get => speed; }
    public int Cost { get => cost; }
    public string SpecialEffects { get => specialEffects; }
}