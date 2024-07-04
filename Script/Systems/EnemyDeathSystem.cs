using UnityEngine;
using System;
using Unity.VisualScripting;

public class EnemyDeathSystem : MonoBehaviour
{
    public static EnemyDeathSystem Instance { get; private set; }

    [SerializeField] private int coinsDroppedOnDeath;
    [SerializeField] private int maxDropDistance;
    [SerializeField] private GameObject coinPrefab;


    public event EventHandler<OnDeathEventArgs> OnEnemyDeath;
    public class OnDeathEventArgs : EventArgs {
        public GameObject DeadObject { get; set; }
    }
  

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;

        OnEnemyDeath += LoseCoinsOnDeath;
    }


    public void Die(GameObject deadObject) {
        Debug.Log("Enemy dieded!");
        OnEnemyDeath?.Invoke(this, new OnDeathEventArgs {DeadObject = deadObject});

        // Show Enemy Death Animation
        Destroy(deadObject);

        
    }


    private void LoseCoinsOnDeath(object sender, OnDeathEventArgs e) {
        // Update UI if its a player, else just drop coins
        CoinDropSystem.Instance.DropCoins(e.DeadObject, coinsDroppedOnDeath, maxDropDistance, coinPrefab);
    }

}