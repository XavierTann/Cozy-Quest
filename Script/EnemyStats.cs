using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class EnemyStats : MonoBehaviour, IStats {
    [SerializeField] private float enemyMaxHealth;
    [SerializeField] private float enemyAttackDamage;
    [SerializeField] private float enemyAttackRange;

    private float enemyHealth;

    private void Awake()
    {
        // Initialize the enemy's health to the maximum health value
        enemyHealth = enemyMaxHealth;
    }

    public float GetHealth() {
        return enemyHealth;
    }

    public void SetHealth(float enemyHealth) {
        this.enemyHealth = enemyHealth;
    }

    public float GetAttackDamage() {
        return enemyAttackDamage;
    }

    public float GetAttackRange() {
        return enemyAttackRange;
    }

    public float GetMaxHealth()
    {
        return enemyMaxHealth;
    }
}