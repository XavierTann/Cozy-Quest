using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour
{
    public void SetCoinCount(int coin) {
        GetComponentInChildren<TextMeshProUGUI>().SetText(coin.ToString());
    }
}
