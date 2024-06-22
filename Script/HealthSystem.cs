using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private float maxHealth;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject healthBar;
    
    private PlayerController playerController;
    private float currentHealth;

    
    private float enemyDamage = 10f;
    private float enemyRange = 0.5f;
    
    private void Start() {
        playerController = GetComponent<PlayerController>();
        currentHealth = maxHealth;
        healthBar.GetComponent<HealthBar>().SetMaxHealth(maxHealth);

    }

    private void Update() {
        // if collide with enemy
        if (EnemyAttacks()) {
            TakeDamage();
        }

    }

    private bool EnemyAttacks()
    {
        if (Physics2D.OverlapCircle(playerController.TargetPos, enemyRange, enemyLayer)) {
            return true;
        }
        return false;
    }

    private void TakeDamage() {
        currentHealth -= enemyDamage * Time.deltaTime;
        Debug.Log(currentHealth);
        healthBar.GetComponent<HealthBar>().SetHealth(currentHealth);
    }
        
}