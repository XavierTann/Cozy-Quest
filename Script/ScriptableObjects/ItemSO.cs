using System;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    [SerializeField]
    private new string name;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    private int cost;

    [SerializeField]
    private bool isStackable;

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
    public virtual bool IsStackable
    {
        get => isStackable;
    }
}
