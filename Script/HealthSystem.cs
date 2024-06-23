using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private float maxHealth;
    [SerializeField] private GameObject healthBar;
    
    private float currentHealth;
    private HealthBarUI healthBarUI;

    private void Awake() {
        currentHealth = maxHealth;
        InitializeHealthBarUI();
            
    }

    private void InitializeHealthBarUI() {
        healthBar.SetActive(true);
        healthBarUI = healthBar.GetComponent<HealthBarUI>();
        healthBarUI.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(float enemyDamage) {
        currentHealth -= enemyDamage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't drop below 0 or exceed max health

        // Debug.Log("Current Health: " + currentHealth);

        if (healthBarUI != null) {
            healthBarUI.SetHealth(currentHealth);
        }
    }
}
