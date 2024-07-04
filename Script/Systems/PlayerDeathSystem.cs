using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;

public class PlayerDeathSystem : MonoBehaviour 
{
    /*
    Death system manages drop items and coins on death.
    For now, death dont lose equipment, just coins.
    Listens for death event 
    Definition for death event
    Trigger death UI
    */
    public static PlayerDeathSystem Instance { get; private set; }


    [SerializeField] private int coinsDroppedOnDeath;
    [SerializeField] private int maxDropDistance;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private float respawnTimer = 2f;


    public event EventHandler<OnDeathEventArgs> OnPlayerDeath;
    public class OnDeathEventArgs : EventArgs {
        public GameObject DeadObject { get; set; }
    }
  

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;

        OnPlayerDeath += LoseCoinsOnDeath;
    }


    public void Die(GameObject deadObject) {
        Debug.Log("Player dieded!");
        OnPlayerDeath?.Invoke(this, new OnDeathEventArgs {DeadObject = deadObject});

        // Show Death Animation

        // Destroy game object and reinstantiate?
        StartCoroutine(ResetPlayerAfterDelay(deadObject, respawnTimer));
        
    }


    private void LoseCoinsOnDeath(object sender, OnDeathEventArgs e) {
        // Update UI if its a player, else just drop coins
  
        CoinSystem.Instance.LoseCoins(coinsDroppedOnDeath);
        CoinDropSystem.Instance.DropCoins(e.DeadObject, coinsDroppedOnDeath, maxDropDistance, coinPrefab);
    }

    private IEnumerator ResetPlayerAfterDelay(GameObject deadObject, float respawnTimer) {
        yield return new WaitForSeconds(respawnTimer);
        ResetPlayer(deadObject);
    }

    private void ResetPlayer(GameObject gameObject) {
        gameObject.transform.position = new Vector3(0,0,0);
        gameObject.GetComponent<HealthSystem>().ResetHealth();
        CoinSystem.Instance.ResetCoins();

    }

    
    
}