using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField]
    GameObject[] shopSlotList;

    [SerializeField]
    ItemSO[] shopItemList;

    [SerializeField]
    Button LeaveShopButton;

    [SerializeField]
    Sprite itemSoldSprite;

    [SerializeField]
    private GameObject shopBox;

    private void Start()
    {
        for (int i = 0; i < shopSlotList.Length; i++)
        {
            int index = i;
            shopSlotList[index]
                .GetComponentInChildren<Button>()
                .onClick.AddListener(() =>
                {
                    RemoveItems(shopSlotList[index], shopItemList[index]);
                });
        }

        LeaveShopButton.onClick.AddListener(LeaveShop);
    }

    public void RemoveItems(GameObject shopSlot, ItemSO item)
    {
        bool boughtItem = ShopSystem.Instance.BuyItem(item);
        if (boughtItem)
        {
            shopSlot.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite =
                itemSoldSprite;
        }
        else
        {
            Debug.Log("Insufficient Money");
        }
    }

    public void EnterShop()
    {
        DisplayUI();
        ShopSystem.Instance.InvokeOnShowShop();
    }

    public void DisplayUI()
    {
        for (int i = 0; i < shopSlotList.Length; i++)
        {
            shopSlotList[i].transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite =
                shopItemList[i].Sprite;
            shopSlotList[i].GetComponentInChildren<Text>().text = shopItemList[i].Cost.ToString();
        }

        shopBox.SetActive(true);
    }

    public void LeaveShop()
    {
        shopBox.SetActive(false);
        ShopSystem.Instance.InvokeOnHideShop();
    }
}
