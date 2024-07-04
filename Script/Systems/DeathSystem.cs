using UnityEngine;
using System;

public class DeathSystem : MonoBehaviour 
{
    /*
    Death system manages drop items and coins on death.
    For now, death dont lose equipment, just coins.
    Listens for death event 
    Definition for death event
    Trigger death UI
    */
    public static DeathSystem Instance { get; private set; }


    [SerializeField] private int coinsDroppedOnDeath;
    [SerializeField] private int maxDropDistance;
    [SerializeField] private GameObject coinPrefab;

      public bool isDead = false;

    public event EventHandler<OnDeathEventArgs> OnDeath;
    public class OnDeathEventArgs : EventArgs {
        public GameObject DeadObject { get; set; }
    }
  

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;

        OnDeath += LoseCoinsOnDeath;
    }
    

    public void Die(GameObject deadObject) {
        Debug.Log("Player dieded!");
        OnDeath?.Invoke(this, new OnDeathEventArgs {DeadObject = deadObject});
    }


    private void LoseCoinsOnDeath(object sender, OnDeathEventArgs e) {
        // Update UI if its a player, else just drop coins
        if (e.DeadObject.layer == LayerMask.NameToLayer("Player")) {
            CoinSystem.Instance.LoseCoins(coinsDroppedOnDeath);
        }

        CoinDropSystem.Instance.DropCoins(e.DeadObject, coinsDroppedOnDeath, maxDropDistance, coinPrefab);
    }
    
}