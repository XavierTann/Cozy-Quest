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

    public Dictionary<ItemSO, int> NameDictionary = new();
    public Dictionary<int, ItemSO> IndexDictionary = new();

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
            if (!NameDictionary.ContainsKey(item))
            {
                AddItemToDictionary(item);
            }

            // Display UI
            InventoryUI.Instance.UpdateItems();
        }
        else
        {
            Debug.Log("Not enough space in inventory!");
        }
    }

    private void AddItemToDictionary(ItemSO item)
    {
        NameDictionary[item] = nextKey;
        IndexDictionary[nextKey] = item;
        nextKey += 1;
    }

    public void RemoveItemFromDictionary(ItemSO item)
    {
        int index = NameDictionary[item];
        NameDictionary.Remove(item);
        IndexDictionary.Remove(index);
    }
}
