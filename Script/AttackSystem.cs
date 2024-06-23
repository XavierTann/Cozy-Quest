using System.Diagnostics.Tracing;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class AttackSystem : MonoBehaviour {
    // Attack logic, like applying damage, detecting attacks

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

    public void PerformAttack(GameObject attacker, GameObject attackee)
    {
        IStats attackerStats = attacker.GetComponent<IStats>();
        HealthSystem attackeeHealthSystem = attackee.GetComponent<HealthSystem>();

        if (attackerStats != null && attackeeHealthSystem != null)
        {
            attackeeHealthSystem.TakeDamage(attackerStats.GetAttackDamage());
            Debug.Log(attackee.GetComponent<IStats>().GetHealth());
        }
        else
        {
            Debug.LogError("Attack failed: Components not found.");
        }
    }

    public GameObject DetectAttack(GameObject attacker, LayerMask attackeeLayer) {
        IStats attackerStats = attacker.GetComponent<IStats>();
        var collider = Physics2D.OverlapCircle(attacker.transform.position, attackerStats.GetAttackRange(), attackeeLayer);
        if (collider != null) {
            return collider.gameObject;
        }

        // No object detected
        return null;
    }
    
}