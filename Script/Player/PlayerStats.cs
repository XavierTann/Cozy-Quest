using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats {
    [SerializeField] private float playerMaxHealth;
    [SerializeField] private float playerMaxMana;
    [SerializeField] private float playerAttackDamage;
    [SerializeField] private float playerAttackRange;
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float playerDetectRange;


    private float playerHealth;
    private float playerMana;

    private void Awake()
    {
        // Initialize the enemy's health to the maximum health value
        playerHealth = playerMaxHealth;
    }

    public float MaxHealth {
        get { return playerMaxHealth; }
    }

    public float Health {
        get { return playerHealth; }
        set { playerHealth = Mathf.Clamp(value, 0, playerMaxHealth); } // Ensure health stays within valid range
    }

    public float MaxMana {
        get { return playerMaxMana; }
    }

    public float Mana {
        get { return playerMana; }
        set { playerMana = Mathf.Clamp(value, 0, playerMaxMana); } // Ensure health stays within valid range
    }

    public float AttackDamage {
        get { return playerAttackDamage; }
    }

    public float AttackRange {
        get { return playerAttackRange; }
    }

    public float MovementSpeed {
        get { return playerMovementSpeed; }
    }

    public float DetectRange {
        get { return playerDetectRange; }
    }

    
}