using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour {
    [SerializeField] private GameObject healthBar;
    
    private HealthBarUI healthBarUI;
    private IStats playerStats;

    private bool isDead = false;

    // Event to notify when damage is taken
    public event EventHandler OnDamageTaken;



    private void Awake()
    {
        InitializeHealthBarUI();
    }

    public void ResetHealth() {
        playerStats.Health = playerStats.MaxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float damage)
    {
        float newHealth = playerStats.Health - damage;

        if (newHealth <= 0 && isDead == false) 
        {
            isDead = true;

            // If its a player
            if (gameObject.layer == LayerMask.NameToLayer("Player")) {
                PlayerDeathSystem.Instance.Die(gameObject);

                // Set health to zero in UI
                playerStats.Health = 0;
                UpdateHealthUI();
            }
            else {
                // If it is an enemy
                EnemyDeathSystem.Instance.Die(gameObject);
            }
            
            
        }
        
        else 
        {
            playerStats.Health = newHealth;
            UpdateHealthUI();
            OnDamageTaken?.Invoke(this, EventArgs.Empty);
        }
        
    }

    private void InitializeHealthBarUI() {
        playerStats = gameObject.GetComponent<IStats>();
        healthBar.SetActive(true);
        healthBarUI = healthBar.GetComponent<HealthBarUI>();
        healthBarUI.SetMaxHealth();
    }

    

    public void UpdateHealthUI() {
        healthBarUI.SetHealth(playerStats.Health, playerStats.MaxHealth);
    }
}
