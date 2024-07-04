using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
UI Script manages display of items, and button logic like clicking of equip weapon, and support drag and drop moving of inventory
*/

public class InventoryUI : MonoBehaviour 
{
    public static InventoryUI Instance {get; private set;}

    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private Button hideInventoryButton;

    private List<WeaponSO> weaponSOList = new List<WeaponSO>();

    private void Awake() {
        if (Instance != null && Instance != this ) {
            Destroy(gameObject);
        }
        Instance = this;

        hideInventoryButton.onClick.AddListener(HideDisplay);

    }

    
    public void HandleUpdate() {
        // DisplayItems();
    }

    public void DisplayInventory() {
        inventoryUI.SetActive(true);
        InventorySystem.Instance.OnOpenInventory?.Invoke();
        foreach (WeaponSO weapon in weaponSOList) {
            // Display item on UI
            Debug.Log($"{weapon.name} is displayed");
        }
        Debug.Log("Inventory Displayed!");
    }

    public void UpdateItems() {
        weaponSOList = InventorySystem.Instance.WeaponSOList;

        if (weaponSOList != null) {
            
            foreach (WeaponSO weaponSO in weaponSOList)
            {
            int weaponSOIndex = weaponSOList.IndexOf(weaponSO);

            Transform inventorySlot = inventoryUI.transform.GetChild(0).GetChild(weaponSOIndex);
            inventorySlot.GetComponent<InventorySlot>().SetSprite(weaponSO);
            inventorySlot.GetComponent<InventorySlot>().WeaponSO = weaponSO;

            
            }        
        }
        
    }

    public void HideDisplay() {
        inventoryUI.SetActive(false);
        InventorySystem.Instance.OnHideInventory?.Invoke();
    }

 
}

