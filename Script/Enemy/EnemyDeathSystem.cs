using UnityEngine;
using System;
using Unity.VisualScripting;

public class EnemyDeathSystem : MonoBehaviour
{
    public static EnemyDeathSystem Instance { get; private set; }

    [SerializeField] private int coinsDroppedOnDeath;
    [SerializeField] private int maxDropDistance;
    [SerializeField] private GameObject coinPrefab;

    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private float potionDropChance = 1;


    public event EventHandler<OnDeathEventArgs> OnEnemyDeath;
    public class OnDeathEventArgs : EventArgs {
        public GameObject DeadObject { get; set; }
    }
  

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        Instance = this;

        OnEnemyDeath += DropItemsOnDeath;
    }


    public void Die(GameObject deadObject) {
        OnEnemyDeath?.Invoke(this, new OnDeathEventArgs {DeadObject = deadObject});

        // Show Enemy Death Animation
        Destroy(deadObject);

        // Give player XP for killing
        ExperienceSystem.Instance.IncreaseXP(deadObject.GetComponent<EnemyStats>().XPDrop);

        
    }


    private void DropItemsOnDeath(object sender, OnDeathEventArgs e) {
        // Drop coins
        CoinDropSystem.Instance.DropCoins(e.DeadObject, coinsDroppedOnDeath, maxDropDistance, coinPrefab);

        // Chance to trigger drop potion event
        if (UnityEngine.Random.value < potionDropChance) {
            PotionDropSystem.Instance.DropPotion(e.DeadObject, maxDropDistance, potionPrefab);
        }
    }

}