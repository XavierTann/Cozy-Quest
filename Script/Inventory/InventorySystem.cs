using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

 /* Responsibliites of Inventory System
    Manages deleting items, adding items to inventory, updating items like their durability,
    Connect to open inventory state, 
    Capacity Management: Maximum items
    Item Interaction: Equipping items, using potion, upgrading items


    Should have another equipment system to be in charge of equipping items 
    UI Script manages display of items, and button logic like clicking of equip weapon, and support drag and drop moving of inventory
    */

public class InventorySystem : MonoBehaviour 
{  
    public static InventorySystem Instance {get; private set;}

    public int maxItems = 20;

    private List<WeaponSO> weaponSOList;
    public List<WeaponSO> WeaponSOList{get {return weaponSOList;} }

    public Dictionary<IItems, (int index, int quantity) > NameDictionary = new(); 
    public Dictionary<int, (IItems item, int quantity) > IndexDictionary = new();
    private int nextKey = 0;

    public Action OnOpenInventory;
    public Action OnHideInventory;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject); 
        }
        Instance = this;

        weaponSOList = new List<WeaponSO>();
    }

    // public void AddItems (WeaponSO weapon) {
    //     if (weaponSOList.Count < 20) {
    //         weaponSOList.Add(weapon);
    //         Debug.Log($"{weapon.name} added to Inventory!");

    //         // Display UI
    //         InventoryUI.Instance.UpdateItems();
    //     }
    //     // Else not enough space in inventory
    // }

    private void RemoveItems (IItems item) {
        int index = NameDictionary[item].index;
        NameDictionary.Remove(item);
        IndexDictionary.Remove(index);
        nextKey = index;
    }

    // New dictionary stuff
    public void AddItems (IItems item) {
        if (nextKey < 20) {

            if (item.GetType() is IStackableItems) {
                AddStackableItem(item, nextKey);
            } 
            else {
                AddNonStackableItem(item, nextKey);
            }

            // Display UI
            InventoryUI.Instance.UpdateItems();
        }
        else {
            Debug.Log("Not enough space in inventory!");
            }
    }

    private void AddNonStackableItem(IItems item, int index) {
        NameDictionary[item] = (index, 1);
        IndexDictionary[index] = (item, 1);
        nextKey += 1;
    }

    private void AddStackableItem(IItems item, int index) {
        if (NameDictionary.ContainsKey(item)) {
            var info = NameDictionary[item];
            NameDictionary[item] = (info.index, info.quantity + 1);
        }
        else {
            AddNonStackableItem(item, index);
        }
    }
}