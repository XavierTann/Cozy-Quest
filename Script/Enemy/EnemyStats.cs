using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IStats {
    [SerializeField] private float enemyMaxHealth;
    [SerializeField] private float enemyAttackDamage;
    [SerializeField] private float enemyAttackRange;
    [SerializeField] private float enemyMovementSpeed;
    [SerializeField] private float enemyDetectRange;

    private float enemyHealth;

    public float MaxHealth {
        get { return enemyMaxHealth; }
    }

    public float Health {
        get { return enemyHealth; }
        set { enemyHealth = Mathf.Clamp(value, 0, enemyMaxHealth); } // Ensuring health stays within valid range
    }

    public float AttackDamage {
        get { return enemyAttackDamage; }
    }

    public float AttackRange {
        get { return enemyAttackRange; }
    }

    public float MovementSpeed {
        get { return enemyMovementSpeed; }
    }

    public float DetectRange {
        get { return enemyDetectRange; } 
    }

    private void Awake()
    {
        // Initialize the enemy's health to the maximum health value
        enemyHealth = enemyMaxHealth;
    }
}