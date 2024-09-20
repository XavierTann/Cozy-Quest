using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    public void Collect()
    {
        CoinSystem.Instance.EarnCoins(1);
    }
}
