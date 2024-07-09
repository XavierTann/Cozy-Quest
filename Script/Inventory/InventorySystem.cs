using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

/* Responsibilities of Inventory System
   Manages deleting items, adding items to inventory, updating items like their durability,
   Connect to open inventory state,
   Capacity Management: Maximum items
   Item Interaction: Equipping items, using potion, upgrading items

   Should have another equipment system to be in charge of equipping items
   UI Script manages display of items, and button logic like clicking of equip weapon, and support drag and drop moving of inventory
*/

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance { get; private set; }

    private int nextKey = 0;
    public int maxItems = 20;

    public Dictionary<ItemSO, (int index, int quantity)> NameDictionary = new();
    public Dictionary<int, (ItemSO item, int quantity)> IndexDictionary = new();

    public Action OnOpenInventory;
    public Action OnHideInventory;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void AddItem(ItemSO item)
    {
        if (nextKey < maxItems)
        {
            if (item.IsStackable)
            {
                AddStackableItem(item);
            }
            else
            {
                AddNonStackableItem(item);
            }

            // Display UI
            InventoryUI.Instance.UpdateItems();
        }
        else
        {
            Debug.Log("Not enough space in inventory!");
        }
    }

    private void AddNonStackableItem(ItemSO item)
    {
        NameDictionary[item] = (nextKey, 1);
        IndexDictionary[nextKey] = (item, 1);
        nextKey += 1;
    }

    private void AddStackableItem(ItemSO item)
    {
        if (NameDictionary.ContainsKey(item))
        {
            var info = NameDictionary[item];
            NameDictionary[item] = (info.index, info.quantity + 1);
            IndexDictionary[info.index] = (item, info.quantity + 1);
        }
        else
        {
            NameDictionary[item] = (nextKey, 1);
            IndexDictionary[nextKey] = (item, 1);

            nextKey += 1;
        }
    }

    public void DecreaseItemQuantity(ItemSO item)
    {
        var (index, quantity) = NameDictionary[item];
        NameDictionary[item] = (index, quantity - 1);
        IndexDictionary[index] = (item, quantity - 1);
    }

    public void RemoveItemFromDictionary(ItemSO item)
    {
        int index = NameDictionary[item].index;
        NameDictionary.Remove(item);
        IndexDictionary.Remove(index);
    }
}
