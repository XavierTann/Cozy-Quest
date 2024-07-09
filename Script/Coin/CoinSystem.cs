using System;
using UnityEngine;

public class CoinSystem : MonoBehaviour {

    public static CoinSystem Instance { get; private set; }

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
        
    }

    public void ResetCoins() {
        currentCoins = startingCoins;
        UpdateCoinUI();
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

    public void EarnCoins(int earnings) {
        currentCoins += earnings;
        UpdateCoinUI();
    }

    public void LoseCoins(int loss) {
        currentCoins -= loss;
        UpdateCoinUI();
    }


    public bool HasEnoughMoney(int cost) {
        if (cost > currentCoins) {
            return false;
        }
        return true;
    }

    private void UpdateCoinUI() {
        coinCounter.GetComponent<CoinUI>().SetCoinCount(currentCoins);
    }

    public int CurrentCoins {
    get { return this.currentCoins; }
    set { this.currentCoins = value; }
}
}