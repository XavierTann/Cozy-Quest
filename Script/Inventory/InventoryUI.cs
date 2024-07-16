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
    public static InventoryUI Instance { get; private set; }

    [SerializeField]
    private GameObject inventoryUI;

    [SerializeField]
    private Button hideInventoryButton;

    public Dictionary<ItemSO, int> ItemDictionary;
    public Dictionary<int, ItemSO> IndexDictionary;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;

        hideInventoryButton.onClick.AddListener(HideDisplay);
    }

    private void Start()
    {
        ItemDictionary = InventorySystem.Instance.NameDictionary;
        IndexDictionary = InventorySystem.Instance.IndexDictionary;
    }

    public void HandleUpdate()
    {
        UpdateItems();
        if (Input.GetKeyDown(KeyCode.Escape)) {
            HideDisplay();
        }
    }

    public void DisplayInventory()
    {
        inventoryUI.SetActive(true);
        InventorySystem.Instance.OnOpenInventory?.Invoke();
        Debug.Log("Inventory Displayed!");
    }

    public void UpdateItems()
    {
        ItemDictionary = InventorySystem.Instance.NameDictionary;
        IndexDictionary = InventorySystem.Instance.IndexDictionary;

        if (IndexDictionary != null)
        {
            for (int i = 0; i < 20; i++)
            {
                if (IndexDictionary.ContainsKey(i))
                {
                    Transform inventorySlot = inventoryUI.transform.GetChild(0).GetChild(i);
                    ItemSO item = IndexDictionary[i];
                    inventorySlot.GetComponent<InventorySlot>().UpdateSlotUI(item);
                }
            }
        }
    }

    public void HideDisplay()
    {
        inventoryUI.SetActive(false);
        InventorySystem.Instance.OnHideInventory?.Invoke();
    }
}
