using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Collections;
using UnityEngine.UI;
using UnityEditor.EditorTools;

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

    [SerializeField] private GameObject deathUI;

    private GameObject deadObject;


    public event EventHandler<OnDeathEventArgs> OnPlayerDeath;
    public class OnDeathEventArgs : EventArgs {
        public GameObject DeadObject { get; set; }
    }

    public event Action OnPlayerReset;
  

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;

        OnPlayerDeath += LoseCoinsOnDeath;

        // Listener for Revive Button
        deathUI.GetComponentInChildren<Button>().onClick.AddListener(() => ResetPlayer(deadObject));
    }


    public void Die(GameObject deadObject) {
        Debug.Log("Player dieded!");
        this.deadObject = deadObject;

        OnPlayerDeath?.Invoke(this, new OnDeathEventArgs {DeadObject = deadObject});

        // Show Death Animation

        // Show Death UI
        deathUI.SetActive(true);
        
    }


    private void LoseCoinsOnDeath(object sender, OnDeathEventArgs e) {
        CoinSystem.Instance.LoseCoins(coinsDroppedOnDeath);
        CoinDropSystem.Instance.DropCoins(e.DeadObject, coinsDroppedOnDeath, maxDropDistance, coinPrefab);
    }


    private void ResetPlayer(GameObject gameObject) {
        // Disable death UI
        deathUI.SetActive(false);

        // Reset death status of game object health system
        gameObject.GetComponent<HealthSystem>().isDead = false;

        // Reset position, health, and coins
        gameObject.transform.position = new Vector3(0,0,0);
        gameObject.GetComponent<HealthSystem>().ResetHealth();
        CoinSystem.Instance.ResetCoins();

        // Trigger player reset event
        OnPlayerReset?.Invoke();

    }

    
    
}