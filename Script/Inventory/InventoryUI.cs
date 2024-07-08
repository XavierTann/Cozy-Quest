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

    public Dictionary<IItems, (int index, int quantity) > NameDictionary;
    public Dictionary<int, (IItems item, int quantity) > IndexDictionary;

    private void Awake() {
        if (Instance != null && Instance != this ) {
            Destroy(gameObject);
        }
        Instance = this;

        hideInventoryButton.onClick.AddListener(HideDisplay);
        
    }

    private void Start() {
        NameDictionary = InventorySystem.Instance.NameDictionary;
        IndexDictionary = InventorySystem.Instance.IndexDictionary;
    }
       

    
    public void HandleUpdate() {
        // DisplayItems();
    }

    public void DisplayInventory() {
        inventoryUI.SetActive(true);
        InventorySystem.Instance.OnOpenInventory?.Invoke();
        Debug.Log("Inventory Displayed!");
    }

    public void UpdateItems() {
        NameDictionary = InventorySystem.Instance.NameDictionary;
        IndexDictionary = InventorySystem.Instance.IndexDictionary;

        if (IndexDictionary != null) {
          
            for (int i = 0 ; i < 20 ; i ++) {
                if (IndexDictionary.ContainsKey(i)) {
                    Transform inventorySlot = inventoryUI.transform.GetChild(0).GetChild(i);
                    IItems item = IndexDictionary[i].item;
                    inventorySlot.GetComponent<InventorySlot>().SetSprite(item);
                    inventorySlot.GetComponent<InventorySlot>().Item = item;
                }
                
            }    
        }
        
    }

    public void HideDisplay() {
        inventoryUI.SetActive(false);
        InventorySystem.Instance.OnHideInventory?.Invoke();
    }

 
}

