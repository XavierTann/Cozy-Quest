using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private Button equipButton;

    [SerializeField]
    private Sprite equippedIcon;

    [SerializeField]
    private Button deleteButton;

    [SerializeField]
    private GameObject stackCount;

    [SerializeField]
    private Image itemImage;

    private ItemSO item;
    public ItemSO Item
    {
        get { return item; }
        set { item = value; }
    }

    private void Awake()
    {
        if (equipButton != null)
        {
            equipButton.onClick.AddListener(UseItem);
        }

        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(DeleteItem);
        }
    }

    public void UpdateSlotUI(ItemSO item)
    {
        // Set image sprite
        this.item = item;
        Image imageComponent = gameObject.transform.GetChild(0).GetComponent<Image>();
        if (imageComponent != null)
        {
            imageComponent.sprite = item.Sprite;
            imageComponent.color = Color.white;
        }

        if (item is StackableItemSO stackableItemSO)
        {
            UpdateStackCount(stackableItemSO);
        }
    }

    private void UseItem()
    {
        if (item == null)
        {
            Debug.Log("No Item in slot!");
        }

        if (item is WeaponSO)
        {
            EquipmentManager.Instance.EquipWeapon(item as WeaponSO);
            equipButton.GetComponent<Image>().sprite = equippedIcon;
        }

        if (item is PotionSO potionSO)
        {
            PotionSystem.Instance.UsePotion();

            // If no more potions left, remove sprite from inventory.
            if (PotionSystem.Instance.potionSO.Count == 0)
            {
                DeleteItem();
            }
            else
            {
                UpdateStackCount(potionSO);
            }
        }

        // Two more cases here, if item is armor, and if item is potion
    }

    private void DeleteItem()
    {
        // Remove item from item dictionary because it will keep updating
        InventorySystem.Instance.RemoveItemFromDictionary(item);

        RemoveSprite();

        ClearStackCount();
    }

    private void RemoveSprite()
    {
        // Update UI and item field
        item = null;
        Image imageComponent = itemImage.GetComponent<Image>();
        imageComponent.sprite = null;
        imageComponent.color = HexToColor("70531B");
    }

    private void UpdateStackCount(StackableItemSO stackableItemSO)
    {
        int count = stackableItemSO.Count;
        stackCount.GetComponent<Text>().text = count.ToString();
    }

    private void ClearStackCount()
    {
        stackCount.GetComponent<Text>().text = "";
    }

    public static Color HexToColor(string hex)
    {
        hex = hex.Replace("0x", ""); //in case the string is formatted 0xFFFFFF
        hex = hex.Replace("#", ""); //in case the string is formatted #FFFFFF
        byte a = 255; //assume fully visible unless specified in hex
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Only use alpha if the string has enough characters
        if (hex.Length == 8)
        {
            a = byte.Parse(hex.Substring(6, 2), System.Globalization.NumberStyles.HexNumber);
        }
        return new Color32(r, g, b, a);
    }
}
