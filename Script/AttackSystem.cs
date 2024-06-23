using UnityEngine;

public class AttackSystem : MonoBehaviour {
    // Attack logic, like detecting hits, and applying damage

    [SerializeField] private LayerMask enemyLayer;

    private EnemyStats enemyStats;

    public static AttackSystem Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want it to persist across scenes
        }
        else
        {
            Destroy(gameObject); // Enforce singleton pattern, destroy duplicates
        }
    }

    public void PerformAttack(GameObject player) {
        var collider = Physics2D.OverlapCircle(player.transform.position, 0.2f, enemyLayer);
        if (collider != null) {
            enemyStats = collider.GetComponent<EnemyStats>();
            float oldEnemyHealth = enemyStats.GetEnemyHealth();
            float playerDamage = player.GetComponent<PlayerStats>().GetPlayerAttackDamage();
            float newEnemyHealth = oldEnemyHealth - playerDamage;
            enemyStats.SetEnemyHealth(newEnemyHealth);
            Debug.Log("Enemy Detected!");
            Debug.Log("Enemy Health is " + newEnemyHealth);
        }
        Debug.Log("Enemy not detected");
        
        
        





    }
    
}