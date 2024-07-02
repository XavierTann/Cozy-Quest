using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private WeaponSO weaponSO;
    public WeaponSO WeaponSO {get {return WeaponSO;} set {weaponSO = value;} }

    public void SetSprite(WeaponSO weapon) {
        weaponSO = weapon;
        Image imageComponent = gameObject.GetComponentInChildren<Image>();
        if (imageComponent != null)
    {
        imageComponent.sprite = weapon.Sprite;
    }
    else
    {
        Debug.LogWarning("Image component not found in children of " + gameObject.name);
    }
    }
}
