using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopSystem : MonoBehaviour
{
    public static ShopSystem Instance { get; private set; }

    [SerializeField]
    List<WeaponSO> weaponSOList;

    [SerializeField]
    List<ArmorSO> armorSOList;

    public event Action OnShowShop;
    public event Action OnHideShop;

    private void Awake()
    {
        if (Instance != null & Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public bool BuyItem(ItemSO item)
    {
        if (CoinSystem.Instance.HasEnoughMoney(item.Cost))
        {
            CoinSystem.Instance.SpendCoins(item.Cost);

            // Auto Equip Weapon
            if (item is WeaponSO)
            {
                EquipmentManager.Instance.EquipWeapon(item as WeaponSO);
            }

            if (item is PotionSO)
            {
                PotionSystem.Instance.OnPotionObtained?.Invoke();
            }

            return true;
        }
        return false;
    }

    public void SellItem(WeaponSO weaponSO)
    {
        CoinSystem.Instance.EarnCoins(weaponSOList[0].Cost);
        // If weapon is equipped, unequip it
        if (weaponSO == EquipmentManager.Instance.Weapon)
        {
            EquipmentManager.Instance.UnequipWeapon();
        }
    }

    public void InvokeOnShowShop()
    {
        OnShowShop?.Invoke();
    }

    public void InvokeOnHideShop()
    {
        OnHideShop?.Invoke();
    }
}
