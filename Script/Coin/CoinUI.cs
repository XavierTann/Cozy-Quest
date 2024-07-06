using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public void SetCoinCount(int coin) {
        GetComponentInChildren<Text>().text = coin.ToString();
    }
}
