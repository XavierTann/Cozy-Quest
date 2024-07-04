using System;
using System.Collections;
using System.Collections.Generic;
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

    public Action OnOpenInventory;
    public Action OnHideInventory;


    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject); 
        }
        Instance = this;

        weaponSOList = new List<WeaponSO>();
    }

    public void AddItems (WeaponSO weapon) {
        if (weaponSOList.Count < 20) {
            weaponSOList.Add(weapon);
            Debug.Log($"{weapon.name} added to Inventory!");

            // Display UI
            InventoryUI.Instance.UpdateItems();
        }
        // Else not enough space in inventory
    }

    private void RemoveItems (WeaponSO weapon) {
        if (weaponSOList.Contains(weapon)) {
            weaponSOList.Remove(weapon);
            InventoryUI.Instance.UpdateItems();
        }
    }

}