using System;
using UnityEngine;

public class ItemSO : ScriptableObject, IItems
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int cost;

    public string Name
    {
        get => name;
    }

    public Sprite Sprite
    {
        get => sprite;
    }
    public int Cost
    {
        get => cost;
    }
}
