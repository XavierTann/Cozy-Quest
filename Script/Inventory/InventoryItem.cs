[System.Serializable]
public class InventoryItem
{
    public ItemSO item;
    public int stackCount;

    public InventoryItem(ItemSO item, int initialCount)
    {
        this.item = item;
        this.stackCount = initialCount;
    }
}
