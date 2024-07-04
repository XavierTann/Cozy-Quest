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

    public void TakeDamage(float damage)
    {
        float newHealth = playerStats.Health - damage;

        if (newHealth <= 0 && isDead == false) 
        {
            isDead = true;
            DeathSystem.Instance.Die(gameObject);

            // Set health to zero in UI
            playerStats.Health = 0;
            UpdateHealthUI();
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
