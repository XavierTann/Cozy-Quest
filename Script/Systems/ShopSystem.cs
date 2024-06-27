using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance {get; private set;}

    [SerializeField] List<WeaponSO> weaponSOList;
    [SerializeField] List<ArmorSO> armorSOList;

    public event Action OnShowShop;
    public event Action OnHideShop;

    private void Awake() {
        if (Instance != null & Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public bool BuyItem(WeaponSO weaponSO) {
        if (CoinSystem.Instance.HasEnoughMoney(weaponSO.Cost)) {
            CoinSystem.Instance.SpendCoins(weaponSO.Cost);
            EquipmentManager.Instance.EquipWeapon(weaponSO);
            Debug.Log("Player has bought " + weaponSO.name + "!");
            return true;
        }
        return false;
    }

    public void SellItem(WeaponSO weaponSO) {
        CoinSystem.Instance.EarnCoins(weaponSOList[0].Cost);
        // If weapon is equipped, unequip it
        if (weaponSO == EquipmentManager.Instance.Weapon) {
            EquipmentManager.Instance.UnequipWeapon();
        }
    }

    public void InvokeOnShowShop() {
        OnShowShop?.Invoke();
    }

    public void InvokeOnHideShop() {
        OnHideShop?.Invoke();
    }

}
