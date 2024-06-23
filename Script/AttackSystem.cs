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

    public void PerformAttack(GameObject attacker, GameObject attackee) {
        IStats attackerStats = attacker.GetComponent<IStats>();
        IStats attackeeStats = attackee.GetComponent<IStats>();

        attackeeStats.SetHealth(attackeeStats.GetHealth() - attackerStats.GetAttackDamage());

        // after this use health system to update ui?
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