using System;
using System.Collections.Generic;
using UnityEngine;


public class ShopUI : MonoBehaviour 
{

    [SerializeField] private GameObject shopBox;


    public void AddItems() {
        // Add items to shop UI
    }

    public void RemoveItems() {
        // Remove items from shop UI
    }

    public void DisplayUI() {
        shopBox.SetActive(true);
    }

    public void EnterShop() {
        shopBox.SetActive(true);
        ShopSystem.Instance.InvokeOnShowShop();
    }

    public void LeaveShop() {
        shopBox.SetActive(false);
        ShopSystem.Instance.InvokeOnHideShop();
    }
}
