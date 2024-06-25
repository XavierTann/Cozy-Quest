using System;
using UnityEngine;

public class CoinSystem : MonoBehaviour {
    [SerializeField] private int coinsDroppedOnDeath;
    [SerializeField] private int startingCoins;
    [SerializeField] private GameObject coinCounter;

    private int currentCoins;

    private void Awake() {
        currentCoins = startingCoins;
        UpdateCoinUI();
        
        GetComponent<HealthSystem>().OnDeath += loseCoinsOnDeath;
    }


    private void spendCoins(int cost) {
        currentCoins -= cost;
        UpdateCoinUI();
        Debug.Log("Player has spent " + cost + " coins!");
    }


    private void loseCoinsOnDeath(object sender, EventArgs e) {
        currentCoins -= coinsDroppedOnDeath;
        UpdateCoinUI();
        // Debug.Log("Player died and has lost " + currentCoins + " coins!");
    }

    private void UpdateCoinUI() {
        coinCounter.GetComponent<CoinUI>().SetCoinCount(currentCoins);
    }

    public int GetCurrentCoins {
        get {return this.currentCoins;}
    }
}