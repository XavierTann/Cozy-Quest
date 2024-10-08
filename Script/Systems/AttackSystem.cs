using System.Diagnostics.Tracing;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    // Attack logic, like applying damage, detecting attacks

    public static AttackSystem Instance { get; private set; }

    [SerializeField]
    private LayerMask enemyLayer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
            attackeeHealthSystem.TakeDamage(attackerStats.AttackDamage);
            // Debug.Log(attackee.GetComponent<IStats>().GetHealth());
        }
        else
        {
            Debug.LogError("Attack failed: Components not found.");
        }
    }

    public GameObject DetectAttack(
        GameObject attacker,
        LayerMask attackeeLayer,
        Vector3 faceDirection
    )
    {
        IStats attackerStats = attacker.GetComponent<IStats>();

        Vector2 faceDirection2D = new(faceDirection.x, faceDirection.y);

        Vector2 start = attacker.transform.position;

        Vector2 end = start + faceDirection2D * attackerStats.AttackRange;

        RaycastHit2D hit = Physics2D.Linecast(start, end, attackeeLayer);

        if (hit.collider != null)
        {
            // Debug.Log("Hit!");
            return hit.collider.gameObject;
        }

        // No object detected
        return null;
    }

    public void DetectSpell(SpellSO spell, Vector2 position)
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(
            position,
            spell.AreaOfEffect,
            enemyLayer
        );
        foreach (Collider2D collider2D in collider2Ds)
        {
            collider2D.gameObject.GetComponent<HealthSystem>().TakeDamage(spell.Damage);
        }
    }
}
