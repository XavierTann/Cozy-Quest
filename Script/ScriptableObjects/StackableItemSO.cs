using UnityEngine;

public class StackableItemSO : ItemSO
{
    [SerializeField]
    private int maxStackSize;

    public int MaxStackSize => maxStackSize;

    public int Count = 0;

    public override bool IsStackable => true;
}
