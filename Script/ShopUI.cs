using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ShopUI : MonoBehaviour 
{
    [SerializeField] GameObject item1;
    [SerializeField] GameObject item2;
    [SerializeField] GameObject item3;
    [SerializeField] Button LeaveShopButton;

    [SerializeField] WeaponSO weaponSO1;
    [SerializeField] WeaponSO weaponSO2;
    [SerializeField] WeaponSO weaponSO3;

    [SerializeField] Sprite sprite;

    [SerializeField] private GameObject shopBox;

    private void Start() {
        item1.GetComponentInChildren<Button>().onClick.AddListener(() => {RemoveItems(item1, weaponSO1);});
        item2.GetComponentInChildren<Button>().onClick.AddListener(() => {RemoveItems(item2, weaponSO2);});
        item3.GetComponentInChildren<Button>().onClick.AddListener(() => {RemoveItems(item3, weaponSO3);});
        LeaveShopButton.onClick.AddListener(LeaveShop);
    }


    public void AddItems(GameObject item) {
        // change sprite to empty image
       
    }

    public void RemoveItems(GameObject item, WeaponSO weapon) {
        bool boughtItem = ShopSystem.Instance.BuyItem(weapon);
        if (boughtItem) {
            item.GetComponentInChildren<Image>().sprite = sprite;
            Debug.Log($"Removed Item, {item}");
        }
        else {
            Debug.Log("Insufficient Money");
        }

    }

    public void DisplayUI() {
        item1.GetComponentInChildren<Image>().sprite = weaponSO1.Sprite;
        item2.GetComponentInChildren<Image>().sprite = weaponSO2.Sprite;
        item3.GetComponentInChildren<Image>().sprite = weaponSO3.Sprite;

        item1.GetComponentInChildren<Text>().text = weaponSO1.Cost.ToString();
        item2.GetComponentInChildren<Text>().text = weaponSO2.Cost.ToString();
        item3.GetComponentInChildren<Text>().text = weaponSO3.Cost.ToString();

        shopBox.SetActive(true);

    }

    public void EnterShop() {
        DisplayUI();
        ShopSystem.Instance.InvokeOnShowShop();
    }

    public void LeaveShop() {
        shopBox.SetActive(false);
        ShopSystem.Instance.InvokeOnHideShop();
    }
}
