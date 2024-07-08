using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Button equipButton;
    [SerializeField] private Sprite equippedIcon;
    [SerializeField] private Button deleteButton;

    private IItems item;
    public IItems Item {get {return item;} set {item = value;} }

    private void Awake() {
        if (equipButton != null)
        {
            equipButton.onClick.AddListener(EquipItem);
        }

        if (deleteButton != null)
        {
            deleteButton.onClick.AddListener(DeleteItem);
        }
    }

    public void SetSprite(IItems item) {
        this.item = item;
        Image imageComponent = gameObject.transform.GetChild(0).GetComponent<Image>();
        if (imageComponent != null)
    {
        imageComponent.sprite = item.Sprite;
        imageComponent.color = Color.white;
    }
    else
    {
        Debug.LogWarning("Image component not found in children of " + gameObject.name);
    }
    }

    private void EquipItem() {      
        if (item == null) {
            Debug.Log("No Weapon in slot!");
        }
        
        if (item is WeaponSO) {
            EquipmentManager.Instance.EquipWeapon(item as WeaponSO);
            equipButton.GetComponent<Image>().sprite = equippedIcon;
        }

        // Two more cases here, if item is armor, and if item is potion

    
    }

    private void DeleteItem() {
        item = null;
        Image imageComponent = gameObject.transform.GetChild(0).GetComponent<Image>();
        imageComponent.sprite = null;
        imageComponent.color = HexToColor("70531B");

        Debug.Log("Delete Item!");
    }

    public static Color HexToColor(string hex)
	{
		hex = hex.Replace ("0x", "");//in case the string is formatted 0xFFFFFF
		hex = hex.Replace ("#", "");//in case the string is formatted #FFFFFF
		byte a = 255;//assume fully visible unless specified in hex
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		//Only use alpha if the string has enough characters
		if(hex.Length == 8){
			a = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
		}
		return new Color32(r,g,b,a);
    }
}
