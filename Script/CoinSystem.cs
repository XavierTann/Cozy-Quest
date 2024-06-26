using System;
using UnityEngine;

public class CoinSystem : MonoBehaviour {

    public static CoinSystem Instance { get; private set; }

    [SerializeField] private int coinsDroppedOnDeath;
    [SerializeField] private int startingCoins;
    [SerializeField] private GameObject coinCounter;

    private int currentCoins;

    private void Awake() {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        currentCoins = startingCoins;
        UpdateCoinUI();
        
        GetComponent<HealthSystem>().OnDeath += LoseCoinsOnDeath;
    }


    public void SpendCoins(int cost) {
        if (HasEnoughMoney(cost)) {
            currentCoins -= cost;
            UpdateCoinUI();
            Debug.Log("Player has spent " + cost + " coins!");
        }
        else {
            Debug.Log("Player has not enough money!");
        }
        
    }

    public void EarnCoins(int cost) {
        currentCoins += cost;
        UpdateCoinUI();
        Debug.Log("Player has earned " + cost + " coins!");
    }


    private void LoseCoinsOnDeath(object sender, EventArgs e) {
        currentCoins -= coinsDroppedOnDeath;
        UpdateCoinUI();
        // Debug.Log("Player died and has lost " + currentCoins + " coins!");
    }

    private bool HasEnoughMoney(int cost) {
        if (cost > currentCoins) {
            return false;
        }
        return true;
    }

    private void UpdateCoinUI() {
        coinCounter.GetComponent<CoinUI>().SetCoinCount(currentCoins);
    }

    public int GetCurrentCoins {
        get {return this.currentCoins;}
    }
}