using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IStats {
    [SerializeField] private float playerMaxHealth;
    [SerializeField] private float playerAttackDamage;
    [SerializeField] private float playerAttackRange;

    private float playerHealth;

    private void Awake()
    {
        // Initialize the enemy's health to the maximum health value
        playerHealth = playerMaxHealth;
    }

    public float GetMaxHealth() {
        return playerMaxHealth;
    }

    public float GetHealth() {
        return playerHealth;
    }

    public void SetHealth(float playerHealth) {
        this.playerHealth = playerHealth;
    }

    public float GetAttackDamage() {
        return playerAttackDamage;
    }

    public float GetAttackRange() {
        return playerAttackRange;
    }
}