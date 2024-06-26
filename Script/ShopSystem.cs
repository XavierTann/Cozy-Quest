using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    // Must Link to coin system
    // Must link to scriptable objects for the cost
    // Must link to player stats to equip the weapon

    [SerializeField] List<WeaponSO> weaponSOList;
    [SerializeField] List<ArmorSO> armorSOList;

    public void BuyItem(WeaponSO weaponSO) {
        CoinSystem.Instance.SpendCoins(weaponSO.Cost);
        EquipmentManager.Instance.EquipWeapon(weaponSO);
        Debug.Log("Player has bought " + weaponSO.name + "!");
    }

    public void SellItem(WeaponSO weaponSO) {
        CoinSystem.Instance.EarnCoins(weaponSOList[0].Cost);
        // If weapon is equipped, unequip it
        if (weaponSO == EquipmentManager.Instance.Weapon) {
            EquipmentManager.Instance.UnequipWeapon();
        }
    }

    public void DisplayItems() {
        foreach (WeaponSO weaponSO in weaponSOList) {
            // Display
        }
        foreach (ArmorSO armorSO in armorSOList) {
            // Display
        }
    }

}
