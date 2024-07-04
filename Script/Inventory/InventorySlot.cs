using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Button equipButton;
    [SerializeField] private Sprite equippedIcon;
    [SerializeField] private Button deleteButton;

    private WeaponSO weaponSO;
    public WeaponSO WeaponSO {get {return WeaponSO;} set {weaponSO = value;} }

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

    public void SetSprite(WeaponSO weapon) {
        weaponSO = weapon;
        Image imageComponent = gameObject.transform.GetChild(0).GetComponent<Image>();
        if (imageComponent != null)
    {
        imageComponent.sprite = weapon.Sprite;
        imageComponent.color = Color.white;
    }
    else
    {
        Debug.LogWarning("Image component not found in children of " + gameObject.name);
    }
    }

    private void EquipItem() {      
        if (weaponSO == null) {
            Debug.Log("No Weapon in slot!");
        }
            
        EquipmentManager.Instance.EquipWeapon(weaponSO);

        equipButton.GetComponent<Image>().sprite = equippedIcon;
    
    }

    private void DeleteItem() {
        weaponSO = null;
        Image imageComponent = gameObject.transform.GetChild(0).GetComponent<Image>();
        imageComponent.sprite = null;
        imageComponent.color = hexToColor("70531B");

        Debug.Log("Delete Item!");
    }

    public static Color hexToColor(string hex)
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
